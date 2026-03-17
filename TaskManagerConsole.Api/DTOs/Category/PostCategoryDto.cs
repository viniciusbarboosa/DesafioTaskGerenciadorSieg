using MongoDB.Bson;

namespace TaskManagerConsole.Api.DTOs.Category
{
    public class PostCategoryDto
    {
        public string Name { get; private set; }
        public string Color { get; private set; }
    }
}
