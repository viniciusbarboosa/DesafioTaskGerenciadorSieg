using MongoDB.Bson;
using MongoDB.Driver;
using TaskManagerConsole.Api.Contexts;
using TaskManagerConsole.Api.Models;
using TaskManagerConsole.Api.Repository.Interfaces;

namespace TaskManagerConsole.Api.Repository
{
    public class old_UserRepository:IUserRepository
    {
        private readonly TaskDbContext _dbContext;

        public old_UserRepository(TaskDbContext taskDbContext)
        {
            _dbContext = taskDbContext;
        }

        public List<User> GetUers()
        {
            var usuarioConnection = _dbContext.GetCollection<User>("User");
            var filter = Builders<User>.Filter.Empty;
            List<User> listUsers = usuarioConnection.Find(filter).ToList();
            return listUsers;
        }

        public User GetUserByName(string name)
        {
            var usuarionConnection = _dbContext.GetCollection<User>("User");
            var filter = Builders<User>.Filter.Eq(i => i.Name, name);
            User user = usuarionConnection.Find(filter).FirstOrDefault();
            return user;
        }
        public User GetUserById(string id)
        {
            var usuarioConnection = _dbContext.GetCollection<User>("User");
            var filter = Builders<User>.Filter.Eq(i => i.ObjectId,new ObjectId(id));
            User user = usuarioConnection.Find(filter).FirstOrDefault();
            return user;
        }

        public void CreateUser(User user)
        {
            var usuarioConnection = _dbContext.GetCollection<User>("User");
            usuarioConnection.InsertOne(user);
        }

        

    }
}
