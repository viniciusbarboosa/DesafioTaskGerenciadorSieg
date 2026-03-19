using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using TaskManagerConsole.Api.Models.Types;
using TaskManagerConsole.Api.Repository.Interfaces.Generic.Models;

namespace TaskManagerConsole.Api.Models
{
    public class User: IEntity
    {
        public User(string name,string email) { 
            Name = name; 
            Email = email;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public ObjectId ObjectId { get; init; }
        [JsonInclude]
        public string Name { get; init; }
        
        public string Email { get; private set; }
    }
}
