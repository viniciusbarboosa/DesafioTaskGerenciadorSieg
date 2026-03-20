using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using TaskManagerConsole.Api.Models.Types;
using TaskManagerConsole.Api.Repository.Interfaces.Generic.Models;

namespace TaskManagerConsole.Api.Models
{
    [BsonIgnoreExtraElements]
    public class User: IEntity
    {
        
        public User(string name,string email) { 
            Name = name; 
            Email = email;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [JsonInclude]
        public string Name { get; init; }
        
        public string Email { get; private set; }
    }
}
