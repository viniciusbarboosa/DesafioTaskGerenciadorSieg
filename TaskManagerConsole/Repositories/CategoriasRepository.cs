using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TaskManagerConsole.Entities;

namespace TaskManagerConsole.Repositories
{
    public class CategoriasRepository
    {

        public void CriarCategoria(Categoria categoria)
        {
            var caminhoJson = File.ReadAllText("C:\\Users\\Sieg\\Documents\\alura\\TaskManagerConsole\\TaskManagerConsole\\bin\\Debug\\net10.0\\categoria.json");
            var categorias = JsonConvert.DeserializeObject<List<Categoria>>(caminhoJson);
            categorias.Add(categoria);
            var categoriasString = JsonConvert.SerializeObject(categorias);

            var path = Path.Combine("C:\\Users\\Sieg\\Documents\\alura\\TaskManagerConsole\\TaskManagerConsole\\bin\\Debug\\net10.0\\categoria.json");
            File.WriteAllText(path, categoriasString);
        }

        public void atualizarCategoria(List<Categoria> listaTarefas)
        {
            var caminhoJson = File.ReadAllText("C:\\Users\\Sieg\\Documents\\alura\\TaskManagerConsole\\TaskManagerConsole\\bin\\Debug\\net10.0\\categoria.json");
            var tarefasString = JsonConvert.SerializeObject(listaTarefas);

            var path = Path.Combine("C:\\Users\\Sieg\\Documents\\alura\\TaskManagerConsole\\TaskManagerConsole\\bin\\Debug\\net10.0\\categoria.json");
            File.WriteAllText(path, tarefasString);
        }

        public List<Categoria> ListarCategoria()
        {
            var caminhoJson = File.ReadAllText("C:\\Users\\Sieg\\Documents\\alura\\TaskManagerConsole\\TaskManagerConsole\\bin\\Debug\\net10.0\\categoria.json");
            var categorias = JsonConvert.DeserializeObject<List<Categoria>>(caminhoJson);

            foreach (var item in categorias)
            {
                Console.WriteLine(item.Nome);
            }

            return categorias;
        }

        public List<Categoria> pegarCategorias()
        {
            var caminhoJson = File.ReadAllText("C:\\Users\\Sieg\\Documents\\alura\\TaskManagerConsole\\TaskManagerConsole\\bin\\Debug\\net10.0\\categoria.json");
            var categorias = JsonConvert.DeserializeObject<List<Categoria>>(caminhoJson);
            return categorias;
        }

    }

}
