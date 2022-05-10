namespace BankUp.Backend.Infrastructure
{
    public class MongoDbConfiguration
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
    }
}