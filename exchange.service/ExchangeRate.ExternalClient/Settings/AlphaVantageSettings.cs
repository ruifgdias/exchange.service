namespace ExchangeRate.ExternalClient.Settings
{
    public class AlphaVantageSettings
    {
        public static string PropertyName => nameof(AlphaVantageSettings);

        public required string ApiKey { get; init; }
    }
}
