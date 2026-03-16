using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Helpers;
using TaskManagerConsole.Repositories.interfaces;

namespace TaskManagerConsole.Repositories
{
    public class UserRepository : IUserRepository
    {
        public void CreateUser(User user)
        {
            JsonFileHelper.WriteFile(user);
        }

        public List<User> GetUsers()
        {
            List<User> users = JsonFileHelper.GetUsersFile();
            return users;
        }

    }
}
