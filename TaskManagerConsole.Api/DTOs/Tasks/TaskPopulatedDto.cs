using MongoDB.Bson.Serialization.Attributes;
using TaskManagerConsole.Api.Models;

namespace TaskManagerConsole.Api.DTOs.Tasks
{
    [BsonIgnoreExtraElements]
    public class TaskPopulatedDto: TaskManagerConsole.Api.Models.Tasks
    {
        public List<TaskManagerConsole.Api.Models.Category> CategoryDetails { get; set; }
        public List<TaskManagerConsole.Api.Models.User> UserDetails { get; set; }

    }
}
