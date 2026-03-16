using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Helpers;

namespace TaskManagerConsole.Repositories.interfaces
{
    public interface ITaskRepository
    {
        public void CreateTask(Tasks tarefa);

        public void UpdateTasks(List<Tasks> listaTarefas);

        public List<Tasks> GetTasks();
    }
}
