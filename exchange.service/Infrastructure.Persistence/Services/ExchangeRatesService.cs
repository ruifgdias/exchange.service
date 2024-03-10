using ExchangeCurrencyRateEventPublisher;
using ExchangeRate.ExternalClient;
using Infrastructure.CrossCutting.Settings;
using Infrastructure.Persistence.Mappings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Services
{
    public class ExchangeRatesService : IExchangeRatesService
    {
        private readonly IMongoCollection<Models.ExchangeRate> ratesCollection;
        private readonly IExchangeRateClient exchangeRateClient;
        private readonly IExchangeCurrencyRatePublisher exchangeCurrencyRatePublisher;
        private readonly InsertOneOptions InsertOptions;

        public ExchangeRatesService(IOptions<MongoSettings> databaseSettings,
                                    IExchangeRateClient exchangeRateClient,
                                    IExchangeCurrencyRatePublisher exchangeCurrencyRatePublisher)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            this.ratesCollection = mongoDatabase.GetCollection<Models.ExchangeRate>(databaseSettings.Value.CollectionName);
            this.exchangeRateClient = exchangeRateClient;
            this.exchangeCurrencyRatePublisher = exchangeCurrencyRatePublisher;
            this.InsertOptions = new InsertOneOptions() { };
        }

        public async Task<List<Models.ExchangeRate>> GetAsync() =>
            await ratesCollection.Find(_ => true).ToListAsync();

        public async Task<Models.ExchangeRate?> GetAsync(string id) =>
            await ratesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Models.ExchangeRate?> GetAsync(string fromCurrencyCode,
                                                         string toCurrencyCode,
                                                         CancellationToken cancellationToken = default)
        {
            var rate = await ratesCollection.Find(x => x.FromCurrencyCode == fromCurrencyCode.ToUpperInvariant() && x.ToCurrencyCode == toCurrencyCode.ToUpperInvariant())
                                            .FirstOrDefaultAsync(cancellationToken);

            if (rate is not null)
            {
                return rate;
            }

            return await GetExchangeCurrencyRateFromExternalSource(fromCurrencyCode, toCurrencyCode, cancellationToken);
        }

        public async Task CreateAsync(Models.ExchangeRate newRate,
                                      CancellationToken cancellationToken) =>
            await ratesCollection.InsertOneAsync(newRate, this.InsertOptions, cancellationToken);

        public async Task UpdateAsync(string id,
                                      Models.ExchangeRate updatedRate) =>
            await ratesCollection.ReplaceOneAsync(x => x.Id == id, updatedRate);

        public async Task RemoveAsync(string id) =>
            await ratesCollection.DeleteOneAsync(x => x.Id == id);

        private async Task<Models.ExchangeRate?> GetExchangeCurrencyRateFromExternalSource(string fromCurrencyCode,
                                                                                           string toCurrencyCode,
                                                                                           CancellationToken cancellationToken)
        {
            var rateExternalResponse = await this.exchangeRateClient.GetCurrencyExchangeRateAsync(fromCurrencyCode,
                                                                                                  toCurrencyCode,
                                                                                                  cancellationToken);
            if (rateExternalResponse is null)
            {
                return null;
            }

            var newExchangeRate = rateExternalResponse.ToExchangeRate();
            var tasks = new List<Task>
            {
                this.CreateAsync(newExchangeRate, cancellationToken),
                this.exchangeCurrencyRatePublisher.Send(newExchangeRate.ToExchangeRateEvent(), cancellationToken)
            };
            await Task.WhenAll(tasks);

            return newExchangeRate;
        }
    }
}
