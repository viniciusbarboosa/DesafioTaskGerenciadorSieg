using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Helpers;
using TaskManagerConsole.Repositories.interfaces;

namespace TaskManagerConsole.Repositories
{
    public class TaskRepository:IRepository<Tasks>
    {
        public void Create(Tasks task)
        {
            JsonFileHelper.WriteFile(task, "tarefas.json");
        }

        public void Update(List<Tasks> listTask)
        {
            JsonFileHelper.UpdateFile(listTask, "tarefas.json");
        }

        public List<Tasks> Get()
        {
            List<Tasks> tasks = JsonFileHelper.GetFile<Tasks>("tarefas.json");
            return tasks;
        }


    }
}
