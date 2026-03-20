using MongoDB.Bson;

namespace TaskManagerConsole.Api.Repository.Interfaces.Generic.Models
{
    public interface IEntity
    {
        public string Id { get; set; }
        string Name { get; init; }
    }
}
