using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagerConsole.Entities
{
    public class Usuario
    {
        public Usuario(){
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
