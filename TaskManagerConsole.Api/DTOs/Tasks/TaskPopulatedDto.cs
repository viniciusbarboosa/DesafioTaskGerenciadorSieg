using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using TaskManagerConsole.Api.Models.Types;

namespace TaskManagerConsole.Api.DTOs.Tasks
{

    public class TaskPopulatedDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateDue { get; set; }
        public DateTime? DateCompletion { get; set; }
        public DateTime DateCreation { get; set; }
        public StatusTask Status { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdCategory { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdUser { get; set; }

        //public string ObjectIdStr => Id.ToString();
        //public string IdCategoryStr => IdCategory.ToString();
        //public string IdUserStr => IdUser.ToString();

        public DateTime DataDue { get; set; }

        public List<TaskManagerConsole.Api.Models.Category> CategoryDetails { get; set; }
        public List<TaskManagerConsole.Api.Models.User> UserDetails { get; set; }
    }

}