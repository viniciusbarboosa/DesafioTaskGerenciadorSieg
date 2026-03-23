using Moq;
using System.ComponentModel.DataAnnotations;
using TaskManagerConsole.Api.DTOs.Tasks;
using TaskManagerConsole.Api.Models;
using TaskManagerConsole.Api.Repository;
using TaskManagerConsole.Api.Repository.Interfaces;
using TaskManagerConsole.Api.Repository.Interfaces.Generic;
using TaskManagerConsole.Api.Repository.Interfaces.Generic.Interface;
using TaskManagerConsole.Api.Services;

namespace TaskManagerConsole.Test.ApiTests.Tasks;

public class TasksTests
{
    public Mock<ITasksRepository> _tasksRepository;
    public Mock<IGenericRepository<User>> _userRepository;
    public Mock<IGenericRepository<Category>> _categoryRepository;
    public TasksService _taskService;

    [SetUp]
    public void Setup()
    {
        _tasksRepository = new Mock<ITasksRepository>();
        _userRepository = new Mock<IGenericRepository<User>>();
        _categoryRepository = new Mock<IGenericRepository<Category>>();
        _taskService = new TasksService(_tasksRepository.Object,_userRepository.Object,_categoryRepository.Object);
    }

    [Test]
    public async Task CreateTasksErroDueDateLessThanCurrent()
    {
        PostTasksDto postTasksDto = new PostTasksDto()
        {
            Title = "Tarefa Teste",
            Description = "TESTE DESCRIÇÃO",
            DateDue = DateTime.Now.AddDays(-10),
            IdCategory = "69b981064bf33dc8f2af02f7",
            IdUser = "69b981064bf33dc8f2af02f7"
        };
        
        Assert.ThrowsAsync<Exception>(() => _taskService.CreateTasks(postTasksDto));

    }

    [Test]
    public async Task CreateTasksSuccess()
    {
        PostTasksDto postTasksDto = new PostTasksDto()
        {
            Title = "Tarefa Teste",
            Description = "TESTE DESCRIÇÃO",
            DateDue = DateTime.Now.AddDays(1),
            IdCategory = "69b981064bf33dc8f2af02f7",
            IdUser = "69b981064bf33dc8f2af02f7"
        };

        _categoryRepository.Setup(x => x.GetById(postTasksDto.IdCategory,"Category")).ReturnsAsync(new Category("Programação","Rosa") );
        _userRepository.Setup(x => x.GetById(postTasksDto.IdUser, "User")).ReturnsAsync(new User("Vinicius","vinicius@gmail.com"));

        await _taskService.CreateTasks(postTasksDto);

        TaskManagerConsole.Api.Models.Tasks task = new TaskManagerConsole.Api.Models.Tasks(postTasksDto.Title, postTasksDto.Description, postTasksDto.DateDue, postTasksDto.IdCategory, postTasksDto.IdUser);
        _tasksRepository.Verify(r => r.CreateTasks(It.IsAny<TaskManagerConsole.Api.Models.Tasks>()),Times.Once);
    }

}
