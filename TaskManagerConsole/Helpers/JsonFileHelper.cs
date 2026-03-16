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

        public static void WriteFile(Category category)
        {
            CheckFiles();

            var pathJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json"));
            var categorys = JsonConvert.DeserializeObject<List<Category>>(pathJson);
            categorys.Add(category);
            var categorysString = JsonConvert.SerializeObject(categorys);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json");
            File.WriteAllText(path, categorysString);
        }

        public static void WriteFile(Tasks task)
        {
            CheckFiles();

            var pathJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json"));
            var tasks = JsonConvert.DeserializeObject<List<Tasks>>(pathJson);
            tasks.Add(task);
            var tasksString = JsonConvert.SerializeObject(tasks);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json");
            File.WriteAllText(path, tasksString);
        }

        public static void WriteFile(User user)
        {
            CheckFiles();

            var pathJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuario.json"));
            var users = JsonConvert.DeserializeObject<List<User>>(pathJson);
            users.Add(user);
            var usersString = JsonConvert.SerializeObject(users);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuario.json");
            File.WriteAllText(path, usersString);
        }

        public static void UpdateFile(List<Category> listCategories)
        {
            CheckFiles();

            var pathJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json"));
            var categorysString = JsonConvert.SerializeObject(listCategories);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json");
            File.WriteAllText(path, categorysString);
        }

        public static void UpdateFile(List<Tasks> listTasks)
        {
            CheckFiles();

            var pathJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json"));
            var tasksString = JsonConvert.SerializeObject(listTasks);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json");
            File.WriteAllText(path, tasksString);
        }

      
        public static List<Category> GetCategoriesFile()
        {
            CheckFiles();

            var pathJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json"));
            var categorys = JsonConvert.DeserializeObject<List<Category>>(pathJson);

            return categorys;
        }

        public static List<User> GetUsersFile()
        {
            CheckFiles();

            var pathJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuario.json"));
            var users = JsonConvert.DeserializeObject<List<User>>(pathJson);

            return users;
        }

        public static List<Tasks> GetTasksFile()
        {
            CheckFiles();

            var pathJson  = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tarefas.json"));
            var tasks = JsonConvert.DeserializeObject<List<Entities.Tasks>>(pathJson);

            return tasks;
        }
    }
}
