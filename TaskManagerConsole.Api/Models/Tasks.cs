using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TaskManagerConsole.Api.Models.Types;

namespace TaskManagerConsole.Api.Models
{
    [BsonIgnoreExtraElements]
    public class Tasks
    {
        public Tasks(string title, string description, DateTime dateDue, string idCategory, string idUser)
        {
            Title = title;
            Description = description;
            DateDue = dateDue;
            DateCreation = DateTime.Now;
            Status = StatusTask.Pendente;
            IdCategory = idCategory;
            IdUser = idUser;
        }

        public Tasks() { }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Title { get; set; }
        public string Description { get;  set; }
        public DateTime DateDue { get; private set; }
        public DateTime? DateCompletion { get; private set; }
        public DateTime DateCreation { get; private set; }
        public StatusTask Status { get; private set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdCategory { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string IdUser { get; set; }

        public void AtualizarTask(string title,string description,DateTime dateDue,StatusTask status,string idCategory,string idUser)
        {
            Title = title;
            Description = description;
            DateDue = dateDue;
            Status = status;
            IdCategory = idCategory;
            IdUser = idUser;
        }

        

    }
}
