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
            try
            {
                _categoryService.CreateCategory(categoryDto);
                return Ok(new { category=categoryDto,message="Categoria Criada com Sucesso" });
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    return BadRequest(new { message = ex.Message });
                }
                return BadRequest(new { message = "Erro ao Criar Categoria" });
            }
        }

        [HttpDelete]
        [Route("{idCategory}")]
        public ActionResult Delete(string idCategory)
        {
            try
            {
                _categoryService.DeleteCategory(idCategory);
                return Ok(new { message = "Categoria Criada Com Sucesso"});
            }catch(Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    return BadRequest(new { message = ex.Message });
                }
                return BadRequest(new { message = "Erro ao Deletar Categoria" });
            }

        }

    }
}
