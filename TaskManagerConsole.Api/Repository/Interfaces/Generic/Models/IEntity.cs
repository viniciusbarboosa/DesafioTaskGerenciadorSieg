using MongoDB.Bson;

namespace TaskManagerConsole.Api.Repository.Interfaces.Generic.Models
{
    public interface IEntity
    {
        ObjectId ObjectId { get; init; }
        string Name { get; init; }
    }
}
