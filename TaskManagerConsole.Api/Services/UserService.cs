using TaskManagerConsole.Api.DTOs.User;
using TaskManagerConsole.Api.Models;
using TaskManagerConsole.Api.Repository;
using TaskManagerConsole.Api.Repository.Interfaces;

namespace TaskManagerConsole.Api.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public List<User> GetUsers()
        {
            List<User> listUsers = _userRepository.GetUers();
            return listUsers;
        }

        public void PostUsers(PostUserDto user)
        {
            if (user.Name == "" || user.Name == null)
            {
                throw new Exception("Usuario não pode ter nome vazio");
            }

            if (user.Email == "" || user.Email == null)
            {
                throw new Exception("Usuario não pode ter Email vazio");
            }

            var userExists = _userRepository.GetUserByName(user.Name);

            if (userExists != null)
            {
                throw new Exception("Já existe o usuario com esse nome");
            }

            User userSave = new User(user.Name,user.Email);

            _userRepository.CreateUser(userSave);
        }
    }
}
