using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using TaskManagerConsole.Api.Models;

namespace TaskManagerConsole.Api.DTOs.Tasks
{

    public class TaskPopulatedDto : TaskManagerConsole.Api.Models.Tasks
    {
        public string ObjectIdStr => Id.ToString();
        public string IdCategoryStr => IdCategory.ToString();
        public string IdUserStr => IdUser.ToString();

        public DateTime DataDue { get; set; }

        public List<TaskManagerConsole.Api.Models.Category> CategoryDetails { get; set; }
        public List<TaskManagerConsole.Api.Models.User> UserDetails { get; set; }
    }

}