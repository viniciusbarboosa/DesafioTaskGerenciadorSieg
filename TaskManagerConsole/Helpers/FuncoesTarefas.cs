using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;

namespace TaskManagerConsole.Helpers
{
    public class FuncoesTarefas
    {
        public static string CaminhoArquivoTarefas()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json");
            return path;
        }

        public static void VerificarArquivoTarefas()
        {
            var path = CaminhoArquivoTarefas();

            if (!File.Exists(path))
            {
                File.WriteAllText(path, "[]");
            }
        }

        public static void EscreverArquivoTarefas(Tarefa tarefa)
        {
            VerificarArquivoTarefas();

            var caminhoJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json"));
            var tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(caminhoJson);
            tarefas.Add(tarefa);
            var tarefasString = JsonConvert.SerializeObject(tarefas);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json");
            File.WriteAllText(path, tarefasString);
        }

        public static void AtualizarArquvioTarefas(List<Tarefa> listaTarefas)
        {
            VerificarArquivoTarefas();

            var caminhoJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json"));
            var tarefasString = JsonConvert.SerializeObject(listaTarefas);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json");
            File.WriteAllText(path, tarefasString);
        }

        public static List<Tarefa> PegarTarefasArquivo()
        {
            VerificarArquivoTarefas();

            var caminhoJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json"));
            var tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(caminhoJson);

            return tarefas;
        }

    }
}
