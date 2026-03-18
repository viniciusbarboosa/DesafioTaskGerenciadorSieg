using Microsoft.VisualBasic;
using MongoDB.Bson;
using System.Net.NetworkInformation;
using TaskManagerConsole.Api.DTOs.Tasks;
using TaskManagerConsole.Api.Models;
using TaskManagerConsole.Api.Models.Types;
using TaskManagerConsole.Api.Repository.Interfaces;
using static System.Net.WebRequestMethods;

namespace TaskManagerConsole.Api.Services
{
    public class TasksService
    {
        public readonly ITasksRepository _tasksRepository;
        public readonly IUserRepository _userRepository;
        public readonly ICategoryRepository _categoryRepository;
        public TasksService(ITasksRepository tasksRepository,IUserRepository userRepository,ICategoryRepository categoryRepository) {
            _tasksRepository = tasksRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public List<Tasks> GetTasks()
        {
            List<Tasks> listTasks = _tasksRepository.GetTasks();
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

            var categoryExists = _categoryRepository.GetCategoryById(taskDto.IdCategory);

            if (categoryExists == null) {
                throw new Exception("Categoria com esse Id não eixste");
            }

            var userExists = _userRepository.GetUserById(taskDto.IdUser);

            if (userExists == null) {
                throw new Exception("Usuario com esse Id não existe");
            }

            var idCategory = new ObjectId(taskDto.IdCategory);
            var idUser = new ObjectId(taskDto.IdUser);
            Tasks task = new Tasks(taskDto.Title, taskDto.Description, taskDto.DateDue,idCategory,idUser);
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

            if((editTaskDto.Status != "Pendente")&&(editTaskDto.Status != "EmAndamento")&&(editTaskDto.Status != "Cancelada"))
            {
                throw new Exception("Status não Disponivel. Status possiveis [Pendente],[EmAndamento],[Cancelada]");
            }

            StatusTask statusTask = StatusTask.Pendente;

            if(editTaskDto.Status == "Pendente")
            {
                statusTask = StatusTask.Pendente;
            }else if(editTaskDto.Status == "EmAndamento")
            {
                statusTask = StatusTask.EmAndamento;
            }else if(editTaskDto.Status == "Cancelada")
            {
                statusTask = StatusTask.Cancelada;
            }
       
            var categoryExists = _categoryRepository.GetCategoryById(editTaskDto.IdCategory);

            if (categoryExists == null)
            {
                throw new Exception("Categoria com esse Id não eixste");
            }

            var userExists = _userRepository.GetUserById(editTaskDto.IdUser);

            if (userExists == null)
            {
                throw new Exception("Usuario com esse Id não existe");
            }

            Tasks taskDb = _tasksRepository.GetById(editTaskDto.ObjectId);

            taskDb.AtualizarTask(editTaskDto.Title,editTaskDto.Description,editTaskDto.DateDue,statusTask, new ObjectId(editTaskDto.IdCategory), new ObjectId(editTaskDto.IdUser));

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

    }
}
