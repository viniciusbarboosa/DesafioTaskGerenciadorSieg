using TaskManagerConsole.Api.DTOs.Category;
using TaskManagerConsole.Api.Models;
using TaskManagerConsole.Api.Repository;
using TaskManagerConsole.Api.Repository.Interfaces;
using MongoDB.Bson;
using TaskManagerConsole.Api.Repository.Interfaces.Generic.Interface;

namespace TaskManagerConsole.Api.Services
{
    public class CategoryService
    {

        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly ITasksRepository _tasksRepository;

        public CategoryService(IGenericRepository<Category> categoryRepository,ITasksRepository tasksRepository)
        {
            _categoryRepository = categoryRepository;
            _tasksRepository = tasksRepository;
        }

        public List<Category> GetCategories() 
        {
            List<Category> listCategories = _categoryRepository.Get("Category");
            return listCategories;
        }

        public void CreateCategory(PostCategoryDto categoryDto)
        {
            if (categoryDto.Name == "" || categoryDto.Name == null)
            {
                throw new Exception("Categoria não pode ter cor Vazia");
            }

            var categoryExits = _categoryRepository.GetByName(categoryDto.Name, "Category");

            if (categoryExits != null)
            {
                throw new Exception("Já existe uma categoria com esse nome");
            }

            Category category = new Category(categoryDto.Name,categoryDto.Color);

            _categoryRepository.Create(category,"Category");

        }

        public void DeleteCategory(string idCategory)
        {
            if (string.IsNullOrEmpty(idCategory))
            {
                throw new Exception("Categoria nao pode ser vazia");
            }

            var listCategoryInTasks = _tasksRepository.GetTasksThatContainCategory(idCategory);
            if(listCategoryInTasks.Count > 0)
            {
                throw new Exception("Não pode apagar Categoria estando ativa nas Tarefas");
            }

            var exitsCategory = _categoryRepository.GetById(idCategory,"Category");
            if(exitsCategory == null)
            {
                throw new Exception("Categoria não existe");
            }

            _categoryRepository.Delete(idCategory,"Category");

        }

    }
}
