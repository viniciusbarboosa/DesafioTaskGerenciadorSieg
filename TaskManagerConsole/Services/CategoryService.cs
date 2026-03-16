using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Repositories;

namespace TaskManagerConsole.Services
{

    public class CategoryService
    {
        CategoryRepository _categoryRepository;
        TaskRepository _taskRepository;

        public CategoryService(CategoryRepository categoryRepository, TaskRepository taskRepository)
        {
            _categoryRepository = categoryRepository;
            _taskRepository = taskRepository;
        }

        public void CreateCategory()
        {
            Console.WriteLine("Digite o nome da Categoria");
            string nameCategory = Console.ReadLine();
            
            if(nameCategory == "")
            {
                Console.WriteLine("Nome da Categoria não pode ser Vazia");
                return;
            }

            bool available = true;
            List<Category> categorys = _categoryRepository.GetCategory();
            foreach (var item in categorys.Select((x, i) => new { Value = x.Name, index = i }))
            {
                if(item.Value == nameCategory)
                {
                    available = false;
                }
            }

            if(available == false)
            {
                Console.WriteLine("Nome de Categoria Já existe não e possivel criar");
                return;
            }

            Console.WriteLine("Digite a cor da Categoria");
            string color = Console.ReadLine();

            

            Category category  = new Category(nameCategory,color);

            _categoryRepository.CreateCategory(category);
        }

        public void GetCategory() {
            Console.WriteLine("LISTAGEM DE CATEGORIAS");
            Console.WriteLine("======================================");
            List<Category> categorys = _categoryRepository.GetCategory();

            foreach (var item in categorys.Select((x, i) => new { Nome = x.Name, Color = x.Color, index = i }))
            {
                Console.WriteLine($" [ {item.index} ] => {item.Nome} | COR {item.Color}");
            }
        }

        public void DeleteCategory()
        {
            //DELETAR CATEGORIA
            Console.WriteLine("LISTAGEM DE CATEGORIAS");
            Console.WriteLine("======================================");
            List<Tasks> tasks = _taskRepository.GetTasks();
            List<Category> categorys = _categoryRepository.GetCategory();

            if (categorys.Count == 0)
            {
                Console.WriteLine("NÃO EXISTEM CATEGORIAS");
            }

            foreach (var item in categorys.Select((x, i) => new { Value = x.Name, index = i }))
            {
                Console.WriteLine($" [ {item.index} ] => {item.Value}");
            }


            Console.WriteLine("Escolha a Categoria que deseja excluir");
            int idChoose;
            while(!int.TryParse(Console.ReadLine(),out idChoose))
            {
                Console.WriteLine("Id Categoria deve ser um inteiro digite novamente um valido");
            }

            if (idChoose < 0 || idChoose >= categorys.Count)
            {
                Console.WriteLine("Categoria Invalida");
                return;
            }

            bool existsTaskCategory = false;

            foreach (var item in tasks.Select((x, i) => new { Value = x.NameCategory, index = i }))
            {
                if (item.Value.ToUpper() == categorys[idChoose].Name.ToUpper())
                {
                    existsTaskCategory = true;
                    Console.WriteLine("Não é possivel apagar Categoria com categoria na lista de Tarefa");
                    return;
                }
            }


            categorys.RemoveAt(idChoose);
            _categoryRepository.UpdatesCategory(categorys);
        }

    }
}
