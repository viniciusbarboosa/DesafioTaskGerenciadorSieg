using MongoDB.Driver;

namespace TaskManagerConsole.Api.Contexts
{
    public class TaskDbContext
    {
        private readonly IMongoDatabase _database;

        public TaskDbContext(IConfiguration configuration)
        {

            var settings = configuration.GetSection("ConnectionStrings:ConnectionTaskManager");
            var client = new MongoClient(settings["ConnectionString"]);
            _database = client.GetDatabase(settings["DatabaseName"]);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            var collection = _database.GetCollection<T>(name);
            return collection;
        }
    }
}
