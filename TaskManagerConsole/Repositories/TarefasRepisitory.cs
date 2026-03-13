using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;

namespace TaskManagerConsole.Repositories
{
    public class TarefasRepisitory
    {
        public void criarTarefa(Tarefa tarefa)
        {
            var caminhoJson = File.ReadAllText("C:\\Users\\Sieg\\Documents\\alura\\TaskManagerConsole\\TaskManagerConsole\\bin\\Debug\\net10.0\\tarefas.json");
            var tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(caminhoJson);
            tarefas.Add(tarefa);
            var tarefasString = JsonConvert.SerializeObject(tarefas);

            var path = Path.Combine("C:\\Users\\Sieg\\Documents\\alura\\TaskManagerConsole\\TaskManagerConsole\\bin\\Debug\\net10.0\\tarefas.json");
            File.WriteAllText(path, tarefasString);
        }

        public void atualizarTarefas(List<Tarefa> listaTarefas)
        {
            var caminhoJson = File.ReadAllText("C:\\Users\\Sieg\\Documents\\alura\\TaskManagerConsole\\TaskManagerConsole\\bin\\Debug\\net10.0\\tarefas.json");
            var tarefasString = JsonConvert.SerializeObject(listaTarefas);

            var path = Path.Combine("C:\\Users\\Sieg\\Documents\\alura\\TaskManagerConsole\\TaskManagerConsole\\bin\\Debug\\net10.0\\tarefas.json");
            File.WriteAllText(path, tarefasString);
        }

        public List<Tarefa> ListarTarefa()
        {
            var caminhoJson = File.ReadAllText("C:\\Users\\Sieg\\Documents\\alura\\TaskManagerConsole\\TaskManagerConsole\\bin\\Debug\\net10.0\\tarefas.json");
            var tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(caminhoJson);

            foreach (var item in tarefas)
            {
                Console.WriteLine($"{item.Titulo} - {item.Descricao} - RESPONSAVEL : {item.NomeUsuario} - DATA VENCIMENTO {item.DataVencimento} - CATEGORIA {item.NomeCategoria}");
            }

            return tarefas;
        }

        public List<Tarefa> pegarTarefas()
        {
            var caminhoJson = File.ReadAllText("C:\\Users\\Sieg\\Documents\\alura\\TaskManagerConsole\\TaskManagerConsole\\bin\\Debug\\net10.0\\tarefas.json");
            var tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(caminhoJson);
            return tarefas;
        }


    }
}
