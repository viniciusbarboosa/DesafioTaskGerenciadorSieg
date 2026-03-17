using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerConsole.Api.DTOs.Category;
using TaskManagerConsole.Api.Repository;
using TaskManagerConsole.Api.Services;

namespace TaskManagerConsole.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        
        public CategoryController(CategoryService categoryService) { 
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult Get() {
            var listCategories = _categoryService.GetCategories();
            return Ok(listCategories);
        }

        [HttpPost]
        public ActionResult Post(PostCategoryDto categoryDto)
        {
            _categoryService.CreateCategory(categoryDto);
            return Ok(categoryDto);
        }

    }
}
