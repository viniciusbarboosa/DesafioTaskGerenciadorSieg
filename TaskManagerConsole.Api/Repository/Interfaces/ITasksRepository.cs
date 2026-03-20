using MongoDB.Bson;
using MongoDB.Driver;
using TaskManagerConsole.Api.DTOs.Tasks;
using TaskManagerConsole.Api.Models;
using TaskManagerConsole.Api.Models.Types;

namespace TaskManagerConsole.Api.Repository.Interfaces
{
    public interface ITasksRepository
    {
        public Task CreateTasks(Tasks task);
        public Task<List<TaskPopulatedDto>> GetTasks();
        public Task<List<TaskPopulatedDto>> GetTasks(int pageNumber ,int pageSize);
        public Task<Tasks> GetById(string id);
        public Task<List<Tasks>> GetTasksThatContainCategory(string objectId);
        public Task DeleteTasks(string idTask);
        public Task EditTask(Tasks task);
        public Task CompleteTasks(string id);
        public Task<List<Tasks>> GetTaskCategory(string idCategory);
        public Task<List<Tasks>> GetTaskStatus(StatusTask statusTask);
        public Task<List<Tasks>> GetTasksOrderedDueDate();
        public Task<List<Tasks>> GetTasksOverdue();
    }

}
