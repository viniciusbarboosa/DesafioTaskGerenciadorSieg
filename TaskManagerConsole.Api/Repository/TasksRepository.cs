using MongoDB.Bson;
using MongoDB.Driver;
using TaskManagerConsole.Api.Contexts;
using TaskManagerConsole.Api.Models;
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
            var filter = Builders<Tasks>.Filter.Eq(i => i.ObjectId,task.ObjectId);
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

        public List<Tasks> GetTasks()
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            var filter = Builders<Tasks>.Filter.Empty;
            List<Tasks> listTasks = taskConnection.Find(filter).ToList();
            return listTasks;
        }

        public Tasks GetById(string id)
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            var filter = Builders<Tasks>.Filter.Eq(i => i.ObjectId,new ObjectId(id));
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

  //      public Tasks GetById(ObjectId id)
  //      {
   //         var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
   //         var filter = Builders<Tasks>.Filter.Eq(i => i.ObjectId, id);
    //        Tasks task = taskConnection.Find(filter).FirstOrDefault();
    //        return task;
    //    }

        public void DeleteTasks(string idTask)
        {
            var taskConnection = _dbContext.GetCollection<Tasks>("Tasks");
            var filter = Builders<Tasks>.Filter.Eq(i => i.ObjectId,new ObjectId(idTask));
            taskConnection.DeleteOne(filter);
        }
    }
}
