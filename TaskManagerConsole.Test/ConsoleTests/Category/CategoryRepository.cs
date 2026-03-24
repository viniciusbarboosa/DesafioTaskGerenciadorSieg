using TaskManagerConsole.Entities;
using TaskManagerConsole.Repositories;

namespace TaskManagerConsole.Test;

public class CategoryRepositoryTests
{
    //anotação essa peguei excluisiva do repositorio pra ver o json
    private CategoryRepository _repository;
    private string _path;

    [SetUp]
    public void Setup()
    {
        _repository = new CategoryRepository();
        _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json");
        File.WriteAllText(_path, "[]");
        //COLOQUEI PRA ZERAR PRA TREINAR O COUNT TBM
    }

    [Test]
    public void Create()
    {
        var category = new Category("Programação", "Roxo");
        _repository.Create(category);

        var result = _repository.Get();

        Assert.That(result.Count, Is.EqualTo(1));
    }

    [Test]
    public void Update()
    {
        var list = new List<Category>()
    {
        new Category("Categoria1", "Azul"),
        new Category("Categoria2", "Verde")
    };

        _repository.Update(list);

        var result = _repository.Get();

        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public void Get()
    {
        var result = _repository.Get();
        Assert.That(result, Is.Not.Null);
    }
}