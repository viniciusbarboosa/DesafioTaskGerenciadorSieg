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
            JsonFileHelper.WriteFile(task);
        }

        public void Update(List<Tasks> listTask)
        {
            JsonFileHelper.UpdateFile(listTask);
        }

        public List<Tasks> Get()
        {
            List<Tasks> tasks = JsonFileHelper.GetTasksFile();
            return tasks;
        }


    }
}
