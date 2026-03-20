using MongoDB.Bson;
using TaskManagerConsole.Api.Repository.Interfaces.Generic.Models;

namespace TaskManagerConsole.Api.Repository.Interfaces.Generic.Interface
{
    public interface IGenericRepository<T> where T : class,IEntity
    {
        public Task<List<T>> Get(string collection);
        public Task<List<T>> GetPaginate(int pageNumber, int pageSize, string collection);
        public Task<T> GetByName(string name, string collection);
        public Task<T> GetById(string id, string collection);
        public Task Delete(string id, string collection);
        public Task Create(T typeClass, string collection);

    }
}
