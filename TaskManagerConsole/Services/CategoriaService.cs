using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Repositories;

namespace TaskManagerConsole.Services
{

    public class CategoriaService
    {
        UsuarioRepository usuarioService = new UsuarioRepository();
        CategoriasRepository categoriaService = new CategoriasRepository();
        TarefasRepisitory tarefasService = new TarefasRepisitory();

        public void criarCategoria()
        {
            Console.WriteLine("Digite o nome da Categoria");
            string nomeCategoria = Console.ReadLine();
            Console.WriteLine("Digite a cor da Categoria");
            string cor = Console.ReadLine();

            Categoria categoria = new Categoria();
            categoria.Nome = nomeCategoria;
            categoria.Cor = cor;

            categoriaService.CriarCategoria(categoria);
        }

        public void listarCategorias() {
            Console.WriteLine("LISTAGEM DE CATEGORIAS");
            Console.WriteLine("======================================");
            categoriaService.ListarCategoria();
        }

        public void deletarCategoria()
        {
            //DELETAR CATEGORIA
            Console.WriteLine("LISTAGEM DE CATEGORIAS");
            Console.WriteLine("======================================");
            List<Tarefa> tarefas = tarefasService.PegarTarefas();
            List<Categoria> categorias = categoriaService.pegarCategorias();

            if (categorias.Count == 0)
            {
                Console.WriteLine("NÃO EXISTEM CATEGORIAS");
            }

            foreach (var item in categorias.Select((x, i) => new { Value = x.Nome, index = i }))
            {
                Console.WriteLine($" [ {item.index} ] => {item.Value}");
            }


            Console.WriteLine("Escolha a Categoria que deseja excluir");
            int idEscolha;
            idEscolha = int.Parse(Console.ReadLine());

            if (idEscolha < 0 || idEscolha >= categorias.Count)
            {
                Console.WriteLine("Categoria Invalida");
                return;
            }

            bool existeTarefaCategoria = false;

            foreach (var item in tarefas.Select((x, i) => new { Value = x.NomeCategoria, index = i }))
            {
                if (item.Value.ToUpper() == categorias[idEscolha].Nome.ToUpper())
                {
                    existeTarefaCategoria = true;
                    Console.WriteLine("Não é possivel apagar Categoria com categoria na lista de Tarefa");
                    return;
                }
            }


            categorias.RemoveAt(idEscolha);
            categoriaService.AtualizarCategoria(categorias);
        }

    }
}
