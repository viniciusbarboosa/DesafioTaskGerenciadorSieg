using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Helpers;

namespace TaskManagerConsole.Repositories.interfaces
{
    public interface IUserRepository
    {
        public void CreateUser(User user);

        public List<User> GetUsers();
    }
}
