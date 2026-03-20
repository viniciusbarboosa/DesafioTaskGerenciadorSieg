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

        public async Task<List<T>> Get(string collection)
        {
            var connection = _dbContext.GetCollection<T>(collection);
            var filter = Builders<T>.Filter.Empty;
            List<T> list = await connection.Find(filter).ToListAsync();
            return list;
        }

        public async Task<List<T>> GetPaginate(int pageNumber, int pageSize,string collection)
        {
            var connection = _dbContext.GetCollection<T>(collection);
            var filter = Builders<T>.Filter.Empty;
            List<T> list = await connection.Find(filter)
                .Skip((pageNumber - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
                
            return list;
        }

        public async Task<T> GetByName(string name,string collection)
        {
            var connection = _dbContext.GetCollection<T>(collection);
            var filter = Builders<T>.Filter.Eq(i => i.Name, name);
            T list = await connection.Find(filter).FirstOrDefaultAsync();
            return list;
        }

        public async Task<T> GetById(string id,string collection)
        {
            var connection = _dbContext.GetCollection<T>(collection);
            var filter = Builders<T>.Filter.Eq(i => i.Id, id);
            T typeClass = await connection.Find(filter).FirstOrDefaultAsync();
            return typeClass;
        }

        public async Task Create(T type,string collection)
        {
            var connection = _dbContext.GetCollection<T>(collection);
            await connection.InsertOneAsync(type);
        }

        public async Task Delete(string id,string collection)
        {
            var connection = _dbContext.GetCollection<T>(collection);
            var filter = Builders<T>.Filter.Eq(i => i.Id, id);
            await connection.DeleteOneAsync(filter);
        }

        
    }
}