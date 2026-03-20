using MongoDB.Bson;
using TaskManagerConsole.Api.Models;
using TaskManagerConsole.Api.Repository.Interfaces.Generic.Models;

namespace TaskManagerConsole.Api.Repository.Interfaces.Generic.Interface
{
    public interface IGenericRepository<T> where T : class,IEntity
    {
        public List<T> Get(string collection);
        public T GetByName(string name, string collection);
        public T GetById(string id, string collection);
        public void Delete(string id, string collection);
        public void Create(T typeClass, string collection);

    }
}
