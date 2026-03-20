using TaskManagerConsole.Api.Repository;
using TaskManagerConsole.Api.Repository.Interfaces;
using MongoDB.Bson;
using TaskManagerConsole.Api.Repository.Interfaces.Generic.Interface;
using TaskManagerConsole.Api.DTOs.Category;
using TaskManagerConsole.Api.Models;

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

        public async Task<List<Category>> GetCategories() 
        {
            List<Category> listCategories = await _categoryRepository.Get("Category");
            return listCategories;
        }

        public async Task<List<Category>> GetCategoriesListPaginate(int pageNumber, int pageSize)
        {
            List<Category> listCategories = await _categoryRepository.GetPaginate(pageNumber,pageSize,"Category");
            return listCategories;
        }

        public async Task CreateCategory(PostCategoryDto categoryDto)
        {
            if (categoryDto.Name == "" || categoryDto.Name == null)
            {
                throw new Exception("Categoria não pode ter cor Vazia");
            }

            var categoryExits = await _categoryRepository.GetByName(categoryDto.Name, "Category");

            if (categoryExits != null)
            {
                throw new Exception("Já existe uma categoria com esse nome");
            }

            Category category = new Category(categoryDto.Name,categoryDto.Color);

            await _categoryRepository.Create(category,"Category");

        }

        public async Task DeleteCategory(string idCategory)
        {
            if (string.IsNullOrEmpty(idCategory))
            {
                throw new Exception("Categoria nao pode ser vazia");
            }

            var listCategoryInTasks = await _tasksRepository.GetTasksThatContainCategory(idCategory);
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
