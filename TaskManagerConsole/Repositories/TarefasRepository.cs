using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Helpers;

namespace TaskManagerConsole.Repositories
{
    public class TarefasRepository
    {
        public void CriarTarefa(Tarefa tarefa)
        {
            FuncoesTarefas.EscreverArquivoTarefas(tarefa);
        }

        public void AtualizarTarefas(List<Tarefa> listaTarefas)
        {
            FuncoesTarefas.AtualizarArquvioTarefas(listaTarefas);
        }

        public List<Tarefa> PegarTarefas()
        {
            List<Tarefa> tarefas = FuncoesTarefas.PegarTarefasArquivo();
            return tarefas;
        }


    }
}
