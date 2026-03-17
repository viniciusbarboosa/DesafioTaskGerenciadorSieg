using MongoDB.Driver;
using TaskManagerConsole.Api.Contexts;
using TaskManagerConsole.Api.Models;
using TaskManagerConsole.Api.Repository.Interfaces;

namespace TaskManagerConsole.Api.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly TaskDbContext _dbContext;

        public CategoryRepository(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Category> GetCategories() {
            var categoryConnection = _dbContext.GetCollection<Category>("Category");
            var filter = Builders<Category>.Filter.Empty;
            List<Category> listCategories = categoryConnection.Find(filter).ToList();
            return listCategories;
        }

        public Category GetCategoryByName(string name)
        {
            var categoryConnection = _dbContext.GetCollection<Category>("Category");
            var filter = Builders<Category>.Filter.Eq(i => i.Name,name);
            Category categorie = categoryConnection.Find(filter).FirstOrDefault();
            return categorie;
        }

        public void CreateCategory(Category category)
        {
            var usuarioConnection = _dbContext.GetCollection<Category>("Category");
            usuarioConnection.InsertOne(category);
        }
    }
}
