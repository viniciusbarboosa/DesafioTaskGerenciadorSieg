using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManagerConsole.Api.Models
{
    public class Category
    {
        public Category(string name,string color) { 
            Name = name;
            Color = color;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public ObjectId ObjectId { get; private set; }
        public string Name { get; private set; }
        public string Color { get; private set; }
    }
}
