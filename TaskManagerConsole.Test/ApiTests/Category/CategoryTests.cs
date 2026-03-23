using Microsoft.AspNetCore.Components;
using Moq;
using TaskManagerConsole.Api.DTOs.Category;
using TaskManagerConsole.Api.Models;
using TaskManagerConsole.Api.Repository;
using TaskManagerConsole.Api.Repository.Interfaces;
using TaskManagerConsole.Api.Repository.Interfaces.Generic;
using TaskManagerConsole.Api.Repository.Interfaces.Generic.Interface;
using TaskManagerConsole.Api.Services;

namespace TaskManagerConsole.Test.ApiTests.Categorys;

public class CategoryTests
{
    private Mock<IGenericRepository<Category>> _categoryRepository;
    private Mock<ITasksRepository> _tasksRepository;
    private CategoryService _categoryService;

    [SetUp]
    public void Setup()
    {
        _categoryRepository = new Mock<IGenericRepository<Category>>();
        _tasksRepository = new Mock<ITasksRepository>();
        _categoryService = new CategoryService(_categoryRepository.Object,_tasksRepository.Object);
    }

    [Test]
    public async Task GetCategoryReturnList()
    {
        List<Category> listCategoryMock = new List<Category>()
        {
            new Category("Programação","Roxa"),
            new Category("estudos","vermelho")
        };

        _categoryRepository.Setup(x => x.Get("Category")).ReturnsAsync(listCategoryMock);

        var listServiceResult = await _categoryService.GetCategories();

        Assert.That(listServiceResult,Is.EqualTo(listCategoryMock));
    }

    [Test]
    public async Task GetCategoryReturnListPaginate()
    {
        List<Category> listCategoryMock = new List<Category>()
        {
            new Category("Programação","Roxa"),
            new Category("estudos","vermelho")
        };

        _categoryRepository.Setup(x => x.GetPaginate(1,10,"Category")).ReturnsAsync(listCategoryMock);

        var listServiceResult = await _categoryService.GetCategoriesListPaginate(1,10);

        Assert.That(listServiceResult,Is.EqualTo(listCategoryMock));
    }

    [Test]
    public async Task CreateCategoryErrorNameNull()
    {
        PostCategoryDto createDto = new PostCategoryDto() { Name = "", Color = "Roxo"};
        Assert.ThrowsAsync<Exception>(() => _categoryService.CreateCategory(createDto));
    }

    [Test]
    public async Task CreateCategorySuccess()
    {
        PostCategoryDto categoryDto = new PostCategoryDto() { Name = "Programação", Color = "Roxo" };
        Category category = new Category(categoryDto.Name, categoryDto.Color);

        await _categoryService.CreateCategory(categoryDto);
        
        _categoryRepository.Verify(r => r.Create(It.IsAny<Category>(),"Category"),Times.Once);
    }

}
