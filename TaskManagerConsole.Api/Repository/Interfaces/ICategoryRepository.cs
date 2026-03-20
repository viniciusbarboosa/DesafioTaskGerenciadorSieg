using MongoDB.Bson;
using MongoDB.Driver;
using TaskManagerConsole.Api.Models;

namespace TaskManagerConsole.Api.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        public List<Category> GetCategories();
        public Category GetCategoryByName(string name);
        public Category GetCategoryById(string id);
        public void DeleteCategory(string id);
        public void CreateCategory(Category category);
    }
}
