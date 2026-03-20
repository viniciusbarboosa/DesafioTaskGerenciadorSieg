using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Net.NetworkInformation;
using TaskManagerConsole.Api.DTOs.Tasks;
using TaskManagerConsole.Api.Models;
using TaskManagerConsole.Api.Models.Types;
using TaskManagerConsole.Api.Repository.Interfaces;
using TaskManagerConsole.Api.Repository.Interfaces.Generic.Interface;
using static System.Net.WebRequestMethods;

namespace TaskManagerConsole.Api.Services
{
    public class TasksService
    {

        //ANOTAÇÃO PRA GOLD NAO COLOQUEI TASK COMO GENERIC PORQUE TEM UNS METODOS BEM ESPECIFICOS
        public readonly ITasksRepository _tasksRepository;
        public readonly IGenericRepository<User> _userRepository;
        public readonly IGenericRepository<Category> _categoryRepository;
        public TasksService(ITasksRepository tasksRepository,IGenericRepository<User> userRepository,IGenericRepository<Category> categoryRepository) {
            _tasksRepository = tasksRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public List<TaskPopulatedDto> GetTasks()
        {
            List<TaskPopulatedDto> listTasks = _tasksRepository.GetTasks();
            return listTasks;
        }

        public void CreateTasks(PostTasksDto taskDto)
        {
            if (string.IsNullOrEmpty(taskDto.Title))
            {
                throw new Exception("Tarefa não pode ser Realizada sem titulo");
            }

            if (string.IsNullOrEmpty(taskDto.IdCategory))
            {
                throw new Exception("id Categoria Não pode ser Vazia");
            }

            if (string.IsNullOrEmpty(taskDto.IdUser))
            {
                throw new Exception("Id usuario deve ser prenchido");
            }

            if (taskDto.DateDue < DateTime.Now)
            {
                throw new Exception("Data não pode ser no passado");
            }

            var categoryExists = _categoryRepository.GetById(taskDto.IdCategory,"Category");

            if (categoryExists == null) {
                throw new Exception("Categoria com esse Id não eixste");
            }

            var userExists = _userRepository.GetById(taskDto.IdUser,"User");

            if (userExists == null) {
                throw new Exception("Usuario com esse Id não existe");
            }

            Tasks task = new Tasks(taskDto.Title, taskDto.Description, taskDto.DateDue, taskDto.IdCategory, taskDto.IdUser);
            _tasksRepository.CreateTasks(task);
        }

        public void EditTask(EditTasksDto editTaskDto)
        {

            if (string.IsNullOrEmpty(editTaskDto.Title))
            {
                throw new Exception("Titulo Não pode ser Vazio");
            }

            if (string.IsNullOrEmpty(editTaskDto.Description))
            {
                throw new Exception("Titulo Não pode ser Vazio");
            }

            if (editTaskDto.DateDue < DateTime.Now)
            {
              throw new Exception("Data nao pode ser Menos que Data Atual");
            }

            if (string.IsNullOrEmpty(editTaskDto.IdCategory)) {
                throw new Exception("Categoria nao pode ser Vazia");
            }

            if (string.IsNullOrEmpty(editTaskDto.IdUser))
            {
                throw new Exception("Usuario nao pode ser Vazio");
            }

            if((editTaskDto.Status.ToUpper() != "PENDENTE") &&(editTaskDto.Status.ToUpper() != "EMANDAMENTO") &&(editTaskDto.Status != "CANCELADA"))
            {
                throw new Exception("Status não Disponivel. Status possiveis [Pendente],[EmAndamento],[Cancelada]");
            }

            StatusTask statusTask = StatusTask.Pendente;

            if (editTaskDto.Status.ToUpper() == "PENDENTE")
            {
                statusTask = StatusTask.Pendente;
            }
            else if (editTaskDto.Status.ToUpper() == "EMANDAMENTO")
            {
                statusTask = StatusTask.EmAndamento;
            }
            else if (editTaskDto.Status.ToUpper() == "CANCELADA")
            {
                statusTask = StatusTask.Cancelada;
            }

            var categoryExists = _categoryRepository.GetById(editTaskDto.IdCategory,"Category");

            if (categoryExists == null)
            {
                throw new Exception("Categoria com esse Id não eixste");
            }

            var userExists = _userRepository.GetById(editTaskDto.IdUser,"User");

            if (userExists == null)
            {
                throw new Exception("Usuario com esse Id não existe");
            }

            Tasks taskDb = _tasksRepository.GetById(editTaskDto.ObjectId);

            taskDb.AtualizarTask(editTaskDto.Title,editTaskDto.Description,editTaskDto.DateDue,statusTask, editTaskDto.IdCategory, editTaskDto.IdUser);

            _tasksRepository.EditTask(taskDb);
        }

        public void DeleteTask(string id)
        {
            Tasks task = _tasksRepository.GetById(id);
            if (task == null)
            {
                throw new Exception("Tarefa com esse id não existe então nao e possiveel excluir");
            }

            if(string.IsNullOrEmpty(id))
            {
                throw new Exception("Erro id não inserido");
            }

            _tasksRepository.DeleteTasks(id);
        }

        public void CompleteTask(string idTask)
        {
            if(idTask == null)
            {
                throw new Exception("Não pode passar tarefa nula.");
            }

            Tasks task = _tasksRepository.GetById(idTask);
            if (task == null)
            {
                throw new Exception("Tarefa com esse id não existe então nao e possiveel excluir");
            }

            _tasksRepository.CompleteTasks(idTask);
        }

        public List<Tasks> ListTaskCategory(string idCategory)
        {
            if (string.IsNullOrEmpty(idCategory))
            {
                throw new Exception("Não pode passar idCategory nulo");
            }

            var categoryExists = _categoryRepository.GetById(idCategory,"Category");

            if (categoryExists == null)
            {
                throw new Exception("Categoria com esse Id não eixste");
            }

            List<Tasks> listTasks = _tasksRepository.GetTaskCategory(idCategory);

            return listTasks;

        }

        public List<Tasks> ListTaskStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                throw new Exception("Não pode passar status nulo");
            }

            StatusTask statusTask = StatusTask.Pendente;

            if (status.ToUpper() == "PENDENTE")
            {
                statusTask = StatusTask.Pendente;
            }
            else if (status.ToUpper() == "EMANDAMENTO")
            {
                statusTask = StatusTask.EmAndamento;
            }
            else if (status.ToUpper() == "CANCELADA")
            {
                statusTask = StatusTask.Cancelada;
            }else if (status.ToUpper() == "CONCLUIDA")
            { 
                statusTask = StatusTask.Concluida;
            }
            else
            {
                throw new Exception("status invalido");
            }

            List<Tasks> listTasks = _tasksRepository.GetTaskStatus(statusTask);
            return listTasks;
        }

        public List<Tasks> GetTasksOrderedDueDate()
        {
            List<Tasks> listTasks = _tasksRepository.GetTasksOrderedDueDate();
            return listTasks;
        }

        public List<Tasks> GetTasksOverdue()
        {
            List<Tasks> listTasks = _tasksRepository.GetTasksOverdue();
            return listTasks;
        }


    }
}
