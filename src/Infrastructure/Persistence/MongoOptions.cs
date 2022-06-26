namespace Infrastructure.Persistence
{
    public class MongoOptions
    {
        public const string Mongo = "Mongo";

        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
