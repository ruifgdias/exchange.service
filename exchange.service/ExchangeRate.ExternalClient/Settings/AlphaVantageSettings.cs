namespace ExchangeRate.ExternalClient.Settings
{
    public class AlphaVantageSettings
    {
        public static string SettingName => nameof(AlphaVantageSettings);

        public required string ApiKey { get; init; }
    }
}
