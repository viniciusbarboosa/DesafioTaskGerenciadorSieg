using TaskManagerConsole.Api.DTOs.Category;
using TaskManagerConsole.Api.Models;
using TaskManagerConsole.Api.Repository;
using TaskManagerConsole.Api.Repository.Interfaces;

namespace TaskManagerConsole.Api.Services
{
    public class CategoryService
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
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

    }
}
