using MongoDB.Bson;
using MongoDB.Driver;
using TaskManagerConsole.Api.Contexts;
using TaskManagerConsole.Api.DTOs.Tasks;
using TaskManagerConsole.Api.Models;
using TaskManagerConsole.Api.Models.Types;
using TaskManagerConsole.Api.Repository.Interfaces;

namespace TaskManagerConsole.Api.Repository
{
    public class TasksRepository : ITasksRepository
    {
        private readonly TaskDbContext _dbContext;

        public TasksRepository(TaskDbContext dbContext) { 
            _dbContext = dbContext;
        }

        public async Task CreateTasks(Tasks task)
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            await taskConnection.InsertOneAsync(task);
        }

        public async Task EditTask(Tasks task)
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            var filter = Builders<Tasks>.Filter.Eq(i => i.Id,task.Id);
            var combineUpdate = Builders<Tasks>.Update.Combine(
            Builders<Tasks>.Update.Set(x => x.Title, task.Title),
            Builders<Tasks>.Update.Set(x => x.Description, task.Description),
            Builders<Tasks>.Update.Set(x => x.DateDue, task.DateDue),
            Builders<Tasks>.Update.Set(x => x.Status, task.Status),
            Builders<Tasks>.Update.Set(x => x.IdCategory, task.IdCategory),
            Builders<Tasks>.Update.Set(x => x.IdUser, task.IdUser)
            );
            await taskConnection.UpdateOneAsync(filter,combineUpdate);
        }

        public async Task<List<TaskPopulatedDto>> GetTasks()
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");

            var query = taskConnection.Aggregate()
              
                .Lookup<Tasks, Category, TaskPopulatedDto>(
                    _dbContext.GetCollection<Category>("Category"),
                    t => t.IdCategory,      
                    c => c.Id,        
                    tp => tp.CategoryDetails 
                )
                .Lookup<TaskPopulatedDto, User, TaskPopulatedDto>(
                    _dbContext.GetCollection<User>("User"),
                    tp => tp.IdUser,        
                    u => u.Id,        
                    tp => tp.UserDetails   
                );

            return await query.ToListAsync();
        }

        public async Task<List<TaskPopulatedDto>> GetTasks(int pageNumber, int pageSize)
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");

            var query = taskConnection.Aggregate()

                .Lookup<Tasks, Category, TaskPopulatedDto>(
                    _dbContext.GetCollection<Category>("Category"),
                    t => t.IdCategory,
                    c => c.Id,
                    tp => tp.CategoryDetails
                )
                .Lookup<TaskPopulatedDto, User, TaskPopulatedDto>(
                    _dbContext.GetCollection<User>("User"),
                    tp => tp.IdUser,
                    u => u.Id,
                    tp => tp.UserDetails
                ).Skip((pageNumber - 1)*pageSize)
                .Limit(pageSize)
                ;

            return await query.ToListAsync();
        }

        public async Task<Tasks> GetById(string id)
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            var filter = Builders<Tasks>.Filter.Eq(i => i.Id,id);
            Tasks task = await taskConnection.Find(filter).FirstOrDefaultAsync();
            return task;
        }

        public async Task<List<Tasks>> GetTasksThatContainCategory(string objectId)
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            var filter = Builders<Tasks>.Filter.Eq(i => i.IdCategory,objectId);
            List<Tasks> listTasks = await taskConnection.Find(filter).ToListAsync();
            return listTasks;
        }

        public async Task DeleteTasks(string idTask)
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            var filter = Builders<Tasks>.Filter.Eq(i => i.Id,idTask);
            await taskConnection.DeleteOneAsync(filter);
        }

        public async Task CompleteTasks(string idTask)
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            var filter = Builders<Tasks>.Filter.Eq(i => i.Id, idTask);
            var combineUpdate = Builders<Tasks>.Update.Combine(
            Builders<Tasks>.Update.Set(x => x.Status,StatusTask.Concluida),
            Builders<Tasks>.Update.Set(x => x.DateCompletion,DateTime.Now)
            );
            await taskConnection.UpdateOneAsync(filter,combineUpdate);
        }

        public async Task<List<Tasks>> GetTaskCategory(string idCategory)
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            var filter = Builders<Tasks>.Filter.Eq(i =>i.IdCategory,idCategory);
            List<Tasks> listTasks = await taskConnection.Find(filter).ToListAsync();
            return listTasks;
        }

        public async Task<List<Tasks>> GetTaskStatus(StatusTask statusTask)
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            var filter = Builders<Tasks>.Filter.Eq(i => i.Status,statusTask);
            List<Tasks> listTasks = await taskConnection.Find(filter).ToListAsync();
            return listTasks;
        }

        public async Task<List<Tasks>> GetTasksOrderedDueDate()
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            var filter = Builders<Tasks>.Filter.Empty;
            List<Tasks> listTasks = await taskConnection.Find(filter).SortBy(i => i.DateDue).ToListAsync();
            return listTasks;
        }

        public async Task<List<Tasks>> GetTasksOverdue()
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            //var filter = Builders<Tasks>.Filter.Lt(i => i.DateDue,DateTime.Now);
            var filter = Builders<Tasks>.Filter.And(Builders<Tasks>.Filter.Lt(i => i.DateDue, DateTime.Now), Builders<Tasks>.Filter.Ne(i => i.Status, StatusTask.Concluida));
            List<Tasks> listTasks = await taskConnection.Find(filter).SortBy(i => i.DateDue).ToListAsync();
            return listTasks;
        }

    }
}
