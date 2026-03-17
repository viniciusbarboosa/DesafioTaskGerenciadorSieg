using MongoDB.Bson;

namespace TaskManagerConsole.Api.DTOs.User
{
    public class GetUserDto
    {
        public ObjectId ObjectId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
