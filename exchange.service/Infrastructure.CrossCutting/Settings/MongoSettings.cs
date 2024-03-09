namespace Infrastructure.CrossCutting.Settings
{
    public class MongoSettings
    {
        public static string PropertyName => nameof(MongoSettings);

        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CollectionName { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
