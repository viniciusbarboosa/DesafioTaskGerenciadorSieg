using TaskManagerConsole.Api.DTOs.User;
using TaskManagerConsole.Api.Models;
using TaskManagerConsole.Api.Repository;
using TaskManagerConsole.Api.Repository.Interfaces;
using TaskManagerConsole.Api.Repository.Interfaces.Generic.Interface;
using TaskManagerConsole.Api.Utils;

namespace TaskManagerConsole.Api.Services
{
    public class UserService
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<User>> GetUsers()
        {
                List<User> listUsers = await _userRepository.Get("User");
                return listUsers;
        }

        public async Task<List<User>> GetUsersListPaginate(int pageNumber, int pageSize)
        {
            List<User> listUsers = await _userRepository.GetPaginate(pageNumber, pageSize, "User");
            return listUsers;
        }

        public async Task PostUsers(PostUserDto user)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                throw new Exception("Usuario não pode ter nome vazio");
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                throw new Exception("Usuario não pode ter Email vazio");
            }

            var mailValid = Validations.IsValidEmail(user.Email);

            if(mailValid == false)
            {
                throw new Exception("Email Invalido");
            }

            var userExists = await _userRepository.GetByName(user.Name,"User");

            if (userExists != null)
            {
                throw new Exception("Já existe o usuario com esse nome");
            }

            User userSave = new User(user.Name,user.Email); 

            await _userRepository.Create(userSave,"User");
        }
    }
}
