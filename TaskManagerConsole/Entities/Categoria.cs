using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagerConsole.Entities
{
    public class Categoria
    {
        public Categoria() { 
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public string Nome { get; set; }
        public string Cor {  get; set; }
    }
}
