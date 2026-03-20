using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TaskManagerConsole.Api.Contexts;
using TaskManagerConsole.Api.DTOs.User;
using TaskManagerConsole.Api.Models;
using TaskManagerConsole.Api.Services;

namespace TaskManagerConsole.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userServices;

        public UserController(UserService userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            try
            {
                List<User> users = await _userServices.GetUsers();
                return Ok(users);
            }
            catch(Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    return BadRequest(new { message = ex.Message });
                }
                return BadRequest(new { message = "Erro ao Listar Usuario" });
            }
            
        }

        [HttpGet("Paginate/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<List<User>>> GetPaginate(int pageNumber,int pageSize)
        {
            try
            {
                List<User> users = await _userServices.GetUsersListPaginate(pageNumber,pageSize);
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
        public async Task<ActionResult<User>> Post(PostUserDto user)
        {
            if(user == null)
            {
                return BadRequest(new { message = "Usuario Não Inserido" });
            }

            try
            {
                await _userServices.PostUsers(user);
                return Ok(user);
            }
            catch (Exception ex) {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    return BadRequest(new { message = ex.Message });
                }
                return BadRequest(new {message = "Erro ao Criar Usuario"});   
            }    
        }


    }
}
