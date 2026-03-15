using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities.Types;

namespace TaskManagerConsole.Entities
{
    public class Tarefa
    {
        public Tarefa()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataVencimento { get; set; }
        public StatusTarefa Status { get; set; }
        public string NomeCategoria { get; set; }
        public string NomeUsuario { get; set; }

    }
}
