using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;

namespace TaskManagerConsole.Helpers
{
    public class JsonFileHelper
    {
        public static string FileCategories()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json");
            return path;
        }

        public static string FileTasks()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json");
            return path;
        }

        public static string FileUsers()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuario.json"); ;
            return path;
        }

        public static void CheckFiles()
        {
            var pathCategories = FileCategories();
            var pathTasks = FileTasks();
            var pathUsers = FileUsers();

            if (!File.Exists(pathCategories))
            {
                File.WriteAllText(pathCategories, "[]");
            }

            if (!File.Exists(pathTasks))
            {
                File.WriteAllText(pathTasks, "[]");
            }

            if (!File.Exists(pathUsers))
            {
                File.WriteAllText(pathUsers,"[]");
            }
        }

        public static void WriteFile<T>(T item,string pathFile)
        {
            CheckFiles();

            var pathJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pathFile));
            var itens = JsonConvert.DeserializeObject<List<T>>(pathJson);
            itens.Add(item);
            var categorysString = JsonConvert.SerializeObject(itens);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pathFile);
            File.WriteAllText(path, categorysString);
        }

        public static void UpdateFile<T>(List<T> listItens, string pathFile)
        {
            CheckFiles();

            var pathJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pathFile));
            var itensString = JsonConvert.SerializeObject(listItens);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pathFile);
            File.WriteAllText(path, itensString);
        }


        public static List<T> GetFile<T>(string pathFile)
        {
            CheckFiles();

            var pathJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pathFile));
            var itens = JsonConvert.DeserializeObject<List<T>>(pathJson);

            return itens;
        }

    }
}
