using MongoDB.Bson;
using MongoDB.Driver;
using TaskManagerConsole.Api.Models;

namespace TaskManagerConsole.Api.Repository.Interfaces
{
    public interface ITasksRepository
    {
        public void CreateTasks(Tasks task);
        public List<Tasks> GetTasks();
        public Tasks GetById(string id);
        public List<Tasks> GetTasksThatContainCategory(ObjectId objectId);
        public void DeleteTasks(string idTask);
        public void EditTask(Tasks task);
    }

}
