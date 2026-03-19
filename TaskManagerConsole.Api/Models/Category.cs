using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TaskManagerConsole.Api.Repository.Interfaces.Generic.Models;

namespace TaskManagerConsole.Api.Models
{
    public class Category: IEntity
    {
        public Category(string name,string color) { 
            Name = name;
            Color = color;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public ObjectId ObjectId { get; init; }
        public string Name { get; init; }
        public string Color { get; private set; }
    }
}
