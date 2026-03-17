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
        public ActionResult<List<User>> Get()
        {
            List<User> users = _userServices.GetUsers();
            return Ok(users);
        }

        [HttpPost]
        public ActionResult<User> Post(PostUserDto user)
        {
            if(user == null)
            {
                return BadRequest("Usuario Não Inserido");
            }

            try
            {
                _userServices.PostUsers(user);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
            
            return Ok(user);
        }

    }
}
