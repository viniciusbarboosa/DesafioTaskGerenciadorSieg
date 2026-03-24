using Moq;
using TaskManagerConsole.Repositories;
using TaskManagerConsole.Repositories.interfaces;
using TaskManagerConsole.Services;

namespace TaskManagerConsole.Test;

public class TasksTests
{
    //FOQUEi no create se nao tiver category e no delete
    private Mock<IRepository<TaskManagerConsole.Entities.User>> _userRepository;
    private Mock<IRepository<TaskManagerConsole.Entities.Category>> _categoryRepository;
    private Mock<IRepository<TaskManagerConsole.Entities.Tasks>> _taskRepository;
    private TaskService _taskService;

    [SetUp]
    public void Setup()
    {
        _userRepository = new Mock<IRepository<TaskManagerConsole.Entities.User>>();
        _categoryRepository = new Mock<IRepository<TaskManagerConsole.Entities.Category>>();
        _taskRepository = new Mock<IRepository<TaskManagerConsole.Entities.Tasks>>();
        _taskService = new TaskService(_userRepository.Object,_categoryRepository.Object,_taskRepository.Object);
    }

    [Test]
    public void CreateTaskSuccessfully()
    {
        var inputs = new StringWriter();
        inputs.WriteLine("Minha Tarefa");      
        inputs.WriteLine("Descricao Teste");   
        inputs.WriteLine("25/12/2026");        
        inputs.WriteLine("0");                 
        inputs.WriteLine("0");                

        var reader = new StringReader(inputs.ToString());
        Console.SetIn(reader);

        var output = new StringWriter();
        Console.SetOut(output);

        List<TaskManagerConsole.Entities.Category> listCategory = new List<TaskManagerConsole.Entities.Category>()
        {
            new TaskManagerConsole.Entities.Category("Programação","Roxo")
        };
        List<TaskManagerConsole.Entities.User> listUser = new List<TaskManagerConsole.Entities.User>()
        {
            new TaskManagerConsole.Entities.User("Vinicius","vinicius@gmail.com")
        };

        _categoryRepository.Setup(x => x.Get()).Returns(listCategory);
        _userRepository.Setup(x => x.Get()).Returns(listUser);

        _taskService.CreateTask();

        _taskRepository.Verify(r => r.Create(It.IsAny< TaskManagerConsole.Entities.Tasks>()), Times.Once);
    }

    [Test]
    public void CreateTaskErroCategory()
    {
        var inputs = new StringWriter();
        var reader = new StringReader(inputs.ToString());
        Console.SetIn(reader);

        var output = new StringWriter();
        Console.SetOut(output);

        List<TaskManagerConsole.Entities.Category> listCategory = new List<TaskManagerConsole.Entities.Category>();
        List<TaskManagerConsole.Entities.User> listUser = new List<TaskManagerConsole.Entities.User>()
    {
        new TaskManagerConsole.Entities.User("teste","tete@gmail")
    };

        _categoryRepository.Setup(x => x.Get()).Returns(listCategory);
        _userRepository.Setup(x => x.Get()).Returns(listUser);

        _taskService.CreateTask();

        _taskRepository.Verify(r => r.Create(It.IsAny<TaskManagerConsole.Entities.Tasks>()), Times.Never);
    }

    [Test]
    public void DeleteTaskSuccess()
    {
        var inputs = new StringWriter();
        inputs.WriteLine("0");

        var reader = new StringReader(inputs.ToString());
        Console.SetIn(reader);

        var output = new StringWriter();
        Console.SetOut(output);

        List<TaskManagerConsole.Entities.Tasks> tasks = new List<TaskManagerConsole.Entities.Tasks>(){
            new TaskManagerConsole.Entities.Tasks("Titulo","TesteDesc",DateTime.Now.AddDays(1),"Programação","Vinicius",TaskManagerConsole.Entities.Types.StatusTask.Pendente)
        };

        _taskRepository.Setup(x => x.Get()).Returns(tasks);

        _taskService.DeleteTask();

        _taskRepository.Verify(r => r.Update(It.IsAny<List<TaskManagerConsole.Entities.Tasks>>()), Times.Once);
    }

    [Test]
    public async Task ListTask()
    {

        List<TaskManagerConsole.Entities.Tasks> tasks = new List<TaskManagerConsole.Entities.Tasks>(){
            new TaskManagerConsole.Entities.Tasks("Titulo","TesteDesc",DateTime.Now.AddDays(1),"Programação","Vinicius",TaskManagerConsole.Entities.Types.StatusTask.Pendente)
        };

        _taskRepository.Setup(r => r.Get()).Returns(tasks);
        _taskService.GetTask();
        _taskRepository.Verify(r => r.Get(),Times.Once);
    }

}
