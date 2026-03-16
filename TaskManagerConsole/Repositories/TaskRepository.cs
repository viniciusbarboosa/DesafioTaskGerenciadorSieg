using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Helpers;
using TaskManagerConsole.Repositories.interfaces;

namespace TaskManagerConsole.Repositories
{
    public class TaskRepository:ITaskRepository
    {
        public void CreateTask(Tasks tarefa)
        {
            JsonFileHelper.WriteFile(tarefa);
        }

        public void UpdateTasks(List<Tasks> listaTarefas)
        {
            JsonFileHelper.UpdateFile(listaTarefas);
        }

        public List<Tasks> GetTasks()
        {
            List<Tasks> tasks = JsonFileHelper.GetTasksFile();
            return tasks;
        }


    }
}
