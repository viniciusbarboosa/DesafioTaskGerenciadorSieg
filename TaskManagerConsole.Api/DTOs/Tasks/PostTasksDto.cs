using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using TaskManagerConsole.Api.Models.Types;

namespace TaskManagerConsole.Api.DTOs.Tasks
{
    public class PostTasksDto
    {
        public string Title { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "A data de vencimento é obrigatória")]
        [DataType(DataType.DateTime)]
        public DateTime DateDue { get; set; }
        public string IdCategory { get; set; }
        public string IdUser { get; set; }
    }
}
