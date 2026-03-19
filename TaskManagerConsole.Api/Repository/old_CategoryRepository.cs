using MongoDB.Bson;
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
            Category category = categoryConnection.Find(filter).FirstOrDefault();
            return category;
        }

        public Category GetCategoryById(string id)
        {
            var categoryConnection = _dbContext.GetCollection<Category>("Category");
            var filter = Builders<Category>.Filter.Eq(i => i.ObjectId, new ObjectId(id));
            Category category = categoryConnection.Find(filter).FirstOrDefault();
            return category;
        }

        public void CreateCategory(Category category)
        {
            var categoryConnection = _dbContext.GetCollection<Category>("Category");
            categoryConnection.InsertOne(category);
        }

        public void DeleteCategory(ObjectId id){
            var categoryConnection = _dbContext.GetCollection<Category>("Category");
            var filter = Builders<Category>.Filter.Eq(i => i.ObjectId, id);
            categoryConnection.DeleteOne(filter);
        }
    }
}
