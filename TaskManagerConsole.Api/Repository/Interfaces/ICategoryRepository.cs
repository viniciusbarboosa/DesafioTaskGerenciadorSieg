using MongoDB.Driver;
using TaskManagerConsole.Api.Models;

namespace TaskManagerConsole.Api.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        public List<Category> GetCategories();
        public Category GetCategoryByName(string name);
        public void CreateCategory(Category category);
    }
}
