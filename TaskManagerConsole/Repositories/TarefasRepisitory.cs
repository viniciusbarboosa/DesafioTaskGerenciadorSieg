using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;

namespace TaskManagerConsole.Repositories
{
    public class TarefasRepisitory
    {
        public void CriarTarefa(Tarefa tarefa)
        {
            var verificarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json"); ;
            if (!File.Exists(verificarPath))
            {
                File.WriteAllText(verificarPath, "[]");
            }

            var caminhoJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json"));
            var tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(caminhoJson);
            tarefas.Add(tarefa);
            var tarefasString = JsonConvert.SerializeObject(tarefas);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json");
            File.WriteAllText(path, tarefasString);
        }

        public void AtualizarTarefas(List<Tarefa> listaTarefas)
        {
            var verificarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json"); ;
            if (!File.Exists(verificarPath))
            {
                File.WriteAllText(verificarPath, "[]");
            }

            var caminhoJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json"));
            var tarefasString = JsonConvert.SerializeObject(listaTarefas);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json");
            File.WriteAllText(path, tarefasString);
        }

        public List<Tarefa> ListarTarefa()
        {
            var verificarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json"); ;
            if (!File.Exists(verificarPath))
            {
                File.WriteAllText(verificarPath, "[]");
            }

            var caminhoJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json"));
            var tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(caminhoJson);

            foreach (var item in tarefas)
            {
                Console.WriteLine($"{item.Titulo} - {item.Descricao} - RESPONSAVEL : {item.NomeUsuario} - DATA VENCIMENTO {item.DataVencimento} - CATEGORIA {item.NomeCategoria}");
            }

            return tarefas;
        }

        public List<Tarefa> PegarTarefas()
        {
            var verificarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json"); ;
            if (!File.Exists(verificarPath))
            {
                File.WriteAllText(verificarPath, "[]");
            }

            var caminhoJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json"));
            var tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(caminhoJson);
            return tarefas;
        }


    }
}
