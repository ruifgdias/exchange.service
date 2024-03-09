namespace Infrastructure.Persistence.Services
{
    public interface IExchangeRatesService
    {
        Task CreateAsync(Models.ExchangeRate newRate,
                         CancellationToken cancellationToken);
        Task<List<Models.ExchangeRate>> GetAsync();
        Task<Models.ExchangeRate?> GetAsync(string id);
        Task<Models.ExchangeRate?> GetAsync(string fromCurrencyCode,
                                            string toCurrencyCode,
                                            CancellationToken cancellation = default);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Models.ExchangeRate updatedRate);
    }
}