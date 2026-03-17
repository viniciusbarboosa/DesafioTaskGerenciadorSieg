using MongoDB.Driver;
using TaskManagerConsole.Api.Models;

namespace TaskManagerConsole.Api.Repository.Interfaces
{
    public interface IUserRepository
    {
        public List<User> GetUers();

        public void CreateUser(User user);

        public User GetUserByName(string name);
    }
}
