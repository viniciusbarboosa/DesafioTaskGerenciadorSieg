using MongoDB.Bson;
using TaskManagerConsole.Api.Models.Types;

namespace TaskManagerConsole.Api.DTOs.Tasks
{
    public class EditTasksDto
    {
        public string ObjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateDue { get; set; }
        public string Status { get; set; }
        public string IdCategory { get; set; }
        public string IdUser { get; set; }
    }
}
