using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TaskManagerConsole.Api.Models.Types;

namespace TaskManagerConsole.Api.Models
{
    [BsonIgnoreExtraElements]
    public class Tasks
    {
        public Tasks(string title, string description, DateTime dateDue, ObjectId idCategory, ObjectId idUser)
        {
            Title = title;
            Description = description;
            DateDue = dateDue;
            DateCreation = DateTime.Now;
            Status = StatusTask.Pendente;
            IdCategory = idCategory;
            IdUser = idUser;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public ObjectId ObjectId { get; set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime DateDue { get; private set; }
        public DateTime? DateCompletion { get; private set; }
        public DateTime DateCreation { get; private set; }
        public StatusTask Status { get; private set; }
        public ObjectId IdCategory { get; private set; }
        public ObjectId IdUser { get; private set; }

        public void AtualizarTask(string title,string description,DateTime dateDue,StatusTask status,ObjectId idCategory,ObjectId idUser)
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
