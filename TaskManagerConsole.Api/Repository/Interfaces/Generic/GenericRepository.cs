using MongoDB.Bson;
using MongoDB.Driver;
using TaskManagerConsole.Api.Contexts;
using TaskManagerConsole.Api.Models;
using TaskManagerConsole.Api.Repository.Interfaces.Generic.Interface;
using TaskManagerConsole.Api.Repository.Interfaces.Generic.Models;

namespace TaskManagerConsole.Api.Repository.Interfaces.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly TaskDbContext _dbContext;

        public GenericRepository(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<T> Get(string collection)
        {
            var connection = _dbContext.GetCollection<T>(collection);
            var filter = Builders<T>.Filter.Empty;
            List<T> list = connection.Find(filter).ToList();
            return list;
        }

        public T GetByName(string name,string collection)
        {
            var connection = _dbContext.GetCollection<T>(collection);
            var filter = Builders<T>.Filter.Eq(i => i.Name, name);
            T list = connection.Find(filter).FirstOrDefault();
            return list;
        }

        public T GetById(string id,string collection)
        {
            var connection = _dbContext.GetCollection<T>(collection);
            var filter = Builders<T>.Filter.Eq(i => i.ObjectId, new ObjectId(id));
            T typeClass = connection.Find(filter).FirstOrDefault();
            return typeClass;
        }

        public void Create(T type,string collection)
        {
            var connection = _dbContext.GetCollection<T>(collection);
            connection.InsertOne(type);
        }

        public void Delete(ObjectId id,string collection)
        {
            var connection = _dbContext.GetCollection<T>(collection);
            var filter = Builders<T>.Filter.Eq(i => i.ObjectId, id);
            connection.DeleteOne(filter);
        }

        
    }
}