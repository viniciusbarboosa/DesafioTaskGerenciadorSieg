using MongoDB.Bson.Serialization.Attributes;
using TaskManagerConsole.Api.Models.Types;
using MongoDB.Bson;

namespace TaskManagerConsole.Api.Models
{
    public class User
    {
        public User(string name,string email) { 
            Name = name; 
            Email = email;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public ObjectId ObjectId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
    }
}
