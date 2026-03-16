using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities.Bases;

namespace TaskManagerConsole.Entities
{
    public class User : BaseEntity
    {
        public User(string name,string email) {
            this.Name = name;
            this.Email = email;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
    }
}
