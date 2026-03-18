using TaskManagerConsole.Api.DTOs.Category;
using TaskManagerConsole.Api.Models;
using TaskManagerConsole.Api.Repository;
using TaskManagerConsole.Api.Repository.Interfaces;
using MongoDB.Bson;

namespace TaskManagerConsole.Api.Services
{
    public class CategoryService
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly ITasksRepository _tasksRepository;

        public CategoryService(ICategoryRepository categoryRepository,ITasksRepository tasksRepository)
        {
            _categoryRepository = categoryRepository;
            _tasksRepository = tasksRepository;
        }

        public List<Category> GetCategories() 
        {
            List<Category> listCategories = _categoryRepository.GetCategories();
            return listCategories;
        }

        public void CreateCategory(PostCategoryDto categoryDto)
        {
            if (categoryDto.Name == "" || categoryDto.Name == null)
            {
                throw new Exception("Categoria não pode ter cor Vazia");
            }

            var categoryExits = _categoryRepository.GetCategoryByName(categoryDto.Name);

            if (categoryExits != null)
            {
                throw new Exception("Já existe uma categoria com esse nome");
            }

            Category category = new Category(categoryDto.Name,categoryDto.Color);

            _categoryRepository.CreateCategory(category);

        }

        public void DeleteCategory(string idCategory)
        {
            if (string.IsNullOrEmpty(idCategory))
            {
                throw new Exception("Categoria nao pode ser vazia");
            }

            var listCategoryInTasks = _tasksRepository.GetTasksThatContainCategory(new ObjectId(idCategory));
            if(listCategoryInTasks.Count > 0)
            {
                throw new Exception("Não pode apagar Categoria estando ativa nas Tarefas");
            }

            var exitsCategory = _categoryRepository.GetCategoryById(idCategory);
            if(exitsCategory == null)
            {
                throw new Exception("Categoria não existe");
            }

            _categoryRepository.DeleteCategory(new ObjectId(idCategory));

        }

    }
}
