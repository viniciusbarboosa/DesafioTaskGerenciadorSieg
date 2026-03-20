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

        public void CreateTasks(Tasks task)
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            taskConnection.InsertOne(task);
        }

        public void EditTask(Tasks task)
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
            taskConnection.UpdateOne(filter,combineUpdate);
        }

        public List<TaskPopulatedDto> GetTasks()
        {
            //TO DE AJUSTAR O ID RETORNA CREIO QUE TENTA TRANSFORMAR EM STRING PELO QUE VI
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

            return query.ToList();
        }

        public Tasks GetById(string id)
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            var filter = Builders<Tasks>.Filter.Eq(i => i.Id,id);
            Tasks task = taskConnection.Find(filter).FirstOrDefault();
            return task;
        }

        public List<Tasks> GetTasksThatContainCategory(ObjectId objectId)
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            var filter = Builders<Tasks>.Filter.Eq(i => i.IdCategory,objectId);
            List<Tasks> listTasks = taskConnection.Find(filter).ToList();
            return listTasks;
        }

        public void DeleteTasks(string idTask)
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            var filter = Builders<Tasks>.Filter.Eq(i => i.Id,idTask);
            taskConnection.DeleteOne(filter);
        }

        public void CompleteTasks(string idTask)
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            var filter = Builders<Tasks>.Filter.Eq(i => i.Id, idTask);
            var combineUpdate = Builders<Tasks>.Update.Combine(
            Builders<Tasks>.Update.Set(x => x.Status,StatusTask.Concluida),
            Builders<Tasks>.Update.Set(x => x.DateCompletion,DateTime.Now)
            );
            taskConnection.UpdateOne(filter,combineUpdate);
        }

        public List<Tasks> GetTaskCategory(ObjectId idCategory)
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            var filter = Builders<Tasks>.Filter.Eq(i =>i.IdCategory,idCategory);
            List<Tasks> listTasks = taskConnection.Find(filter).ToList();
            return listTasks;
        }

        public List<Tasks> GetTaskStatus(StatusTask statusTask)
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            var filter = Builders<Tasks>.Filter.Eq(i => i.Status,statusTask);
            List<Tasks> listTasks = taskConnection.Find(filter).ToList();
            return listTasks;
        }

        public List<Tasks> GetTasksOrderedDueDate()
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            var filter = Builders<Tasks>.Filter.Empty;
            List<Tasks> listTasks = taskConnection.Find(filter).SortBy(i => i.DateDue).ToList();
            return listTasks;
        }

        public List<Tasks> GetTasksOverdue()
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            //var filter = Builders<Tasks>.Filter.Lt(i => i.DateDue,DateTime.Now);
            var filter = Builders<Tasks>.Filter.And(Builders<Tasks>.Filter.Lt(i => i.DateDue, DateTime.Now), Builders<Tasks>.Filter.Ne(i => i.Status, StatusTask.Concluida));
            List<Tasks> listTasks = taskConnection.Find(filter).SortBy(i => i.DateDue).ToList();
            return listTasks;
        }

    }
}
