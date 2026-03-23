using Moq;
using TaskManagerConsole.Api.DTOs.User;
using TaskManagerConsole.Api.Models;
using TaskManagerConsole.Api.Repository.Interfaces.Generic.Interface;
using TaskManagerConsole.Api.Services;
using TaskManagerConsole.Api.Utils;

namespace TaskManagerConsole.Test.ApiTests.Users;

public class UserTests
{
    private Mock<IGenericRepository<User>> _userRepository;
    private UserService _userService;

    [SetUp]
    public void Setup()
    {
        _userRepository = new Mock<IGenericRepository<User>>();
        _userService = new UserService(_userRepository.Object);
    }

    [Test]
    public async Task CreateUserReturnErrorExits()
    {
        PostUserDto userDto = new PostUserDto() { Name = "NomeRepitido", Email = "teste@gmail.com" };
        _userRepository.Setup(r => r.GetByName(userDto.Name, "User")).ReturnsAsync(new User("NomeRepitido", "teste@gmail.com"));
        Assert.ThrowsAsync<Exception>(() => _userService.PostUsers(userDto));

    }

    [Test]
    public async Task CreateUserInvalidEmail()
    {
        PostUserDto userDto = new PostUserDto() { Name = "Nome", Email = "testeemailinvalido" };
        Assert.ThrowsAsync<Exception>(() => _userService.PostUsers(userDto));

    }

    [Test]
    public async Task GetUserReturnList()
    {
        List<User> listUserMock = new List<User>()
        {
            new User("TESTE","teste@gmail.com")
        };

        _userRepository.Setup(x => x.Get("User")).ReturnsAsync(listUserMock);

        var listServiceResult = await _userService.GetUsers();

        Assert.That(listServiceResult[0].Name, Is.EqualTo(listUserMock[0].Name));
    }
}
