using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagerConsole.Entities
{
    public class Tarefa
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataVencimento { get; set; }
        public string Status { get; set; }
        public string NomeCategoria { get; set; }
        public string NomeUsuario { get; set; }

    }
}
