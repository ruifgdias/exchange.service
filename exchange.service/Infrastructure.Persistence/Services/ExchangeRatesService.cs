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
        private readonly InsertOneOptions InsertOptions;

        public ExchangeRatesService(IOptions<MongoSettings> databaseSettings,
            IExchangeRateClient exchangeRateClient)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            this.ratesCollection = mongoDatabase.GetCollection<Models.ExchangeRate>(databaseSettings.Value.CollectionName);
            this.exchangeRateClient = exchangeRateClient;

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
            var rate = await ratesCollection.Find(x => x.FromCurrencyCode == fromCurrencyCode && x.ToCurrencyCode == toCurrencyCode)
                                            .FirstOrDefaultAsync(cancellationToken);

            if (rate is not null)
            {
                return rate;
            }

            var rateExternalResponse = await this.exchangeRateClient.GetCurrencyExchangeRateAsync(fromCurrencyCode, toCurrencyCode, cancellationToken);
            if (rateExternalResponse is null)
            { 
                return null;
            }

            var newExchangeRate = rateExternalResponse.ToExchangeRate();
            await this.CreateAsync(newExchangeRate, cancellationToken);

            return newExchangeRate;
        }

        public async Task CreateAsync(Models.ExchangeRate newRate, CancellationToken cancellationToken) =>
            await ratesCollection.InsertOneAsync(newRate, this.InsertOptions, cancellationToken);

        public async Task UpdateAsync(string id, Models.ExchangeRate updatedRate) =>
            await ratesCollection.ReplaceOneAsync(x => x.Id == id, updatedRate);

        public async Task RemoveAsync(string id) =>
            await ratesCollection.DeleteOneAsync(x => x.Id == id);
    }
}
