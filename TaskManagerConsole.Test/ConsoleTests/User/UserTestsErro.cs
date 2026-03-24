using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Repositories.interfaces;
using TaskManagerConsole.Services;

namespace TaskManagerConsole.Test;

public class UserTestsErro
{
    private Mock<IRepository<User>> _userRepository;
    private UserService _userService;

    [SetUp]
    public void Setup()
    {
        _userRepository = new Mock<IRepository<User>>();
        _userService = new UserService(_userRepository.Object);
    }

    //ANOTAÇÃO: ESSA EU FIZ SO PRA VALIDAR ERROS
    [Test]
    public void CreateUserErroInvalidEmail()
    {
        var inputs = new StringWriter();
        inputs.WriteLine("Vinicius");
        inputs.WriteLine("emailPraNaoPassar");

        Console.SetIn(new StringReader(inputs.ToString()));
        Console.SetOut(new StringWriter());

        _userRepository.Setup(x => x.Get()).Returns(new List<User>());

        _userService.CreateUser();

        _userRepository.Verify(r => r.Create(It.IsAny<User>()), Times.Never);
    }

    [Test]
    public void CreateUserErroDuplicateName()
    {
        var inputs = new StringWriter();
        inputs.WriteLine("Vinicius");
        inputs.WriteLine("vinicius@gmail.com");

        Console.SetIn(new StringReader(inputs.ToString()));
        Console.SetOut(new StringWriter());

        var users = new List<User>()
        {
            new User("Vinicius","outro@email.com")
        };

        _userRepository.Setup(x => x.Get()).Returns(users);

        _userService.CreateUser();

        _userRepository.Verify(r => r.Create(It.IsAny<User>()), Times.Never);
    }
}