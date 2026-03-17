using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Helpers;
using TaskManagerConsole.Repositories.interfaces;

namespace TaskManagerConsole.Repositories
{
    public class UserRepository : IRepository<User>
    {
        public void Create(User user)
        {
            JsonFileHelper.WriteFile(user);
        }

        public List<User> Get()
        {
            List<User> users = JsonFileHelper.GetUsersFile();
            return users;
        }

        public void Update(List<User> listUser)
        {
            JsonFileHelper.UpdateFile(listUser);
        }
    }
}
