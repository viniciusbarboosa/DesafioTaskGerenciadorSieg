using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerConsole.Api.DTOs.Category;
using TaskManagerConsole.Api.Models;
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
        public async Task<ActionResult> Get() {
            try
            {
                var listCategories = await _categoryService.GetCategories();
                return Ok(listCategories);
            }
            catch (Exception ex) {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    return BadRequest(new { message = ex.Message });
                }
                return BadRequest(new { message = "Erro ao Listar Categoria" });
            }

        }

        [HttpGet("Paginate/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<List<User>>> GetPaginate(int pageNumber, int pageSize)
        {
            try
            {
                List<Category> users = await _categoryService.GetCategoriesListPaginate(pageNumber, pageSize);
                return Ok(users);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    return BadRequest(new { message = ex.Message });
                }
                return BadRequest(new { message = "Erro ao Listar Usuario" });
            }

        }

        [HttpPost]
        public async Task<ActionResult> Post(PostCategoryDto categoryDto)
        {
            try
            {
                await _categoryService.CreateCategory(categoryDto);
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
        public async Task<ActionResult> Delete(string idCategory)
        {
            try
            {
                await _categoryService.DeleteCategory(idCategory);
                return Ok(new { message = "Categoria Deletada Com Sucesso"});
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
