using MongoDB.Bson;
using MongoDB.Driver;
using TaskManagerConsole.Api.DTOs.Tasks;
using TaskManagerConsole.Api.Models;
using TaskManagerConsole.Api.Models.Types;

namespace TaskManagerConsole.Api.Repository.Interfaces
{
    public interface ITasksRepository
    {
        public void CreateTasks(Tasks task);
        public List<TaskPopulatedDto> GetTasks();
        public Tasks GetById(string id);
        public List<Tasks> GetTasksThatContainCategory(ObjectId objectId);
        public void DeleteTasks(string idTask);
        public void EditTask(Tasks task);
        public void CompleteTasks(string id);
        public List<Tasks> GetTaskCategory(ObjectId idCategory);
        public List<Tasks> GetTaskStatus(StatusTask statusTask);
        public List<Tasks> GetTasksOrderedDueDate();
        public List<Tasks> GetTasksOverdue();
    }

}
