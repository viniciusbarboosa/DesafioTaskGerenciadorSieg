using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TaskManagerConsole.Entities;

namespace TaskManagerConsole.Services
{
    public class CategoriasService
    {

        public void CriarCategoria(Categoria categoria)
        {
            var caminhoJson = File.ReadAllText("C:\\Users\\Usuario\\Documents\\programacao\\alura\\csharp\\Projetos\\DesafioTaskGerenciadorSieg\\TaskManagerConsole\\bin\\Debug\\net10.0\\categoria.json");
            var categorias = JsonConvert.DeserializeObject<List<Categoria>>(caminhoJson);
            categorias.Add(categoria);
            var categoriasString = JsonConvert.SerializeObject(categorias);

            var path = Path.Combine("C:\\Users\\Usuario\\Documents\\programacao\\alura\\csharp\\Projetos\\DesafioTaskGerenciadorSieg\\TaskManagerConsole\\bin\\Debug\\net10.0\\categoria.json");
            File.WriteAllText(path, categoriasString);
        }

        public List<Categoria> ListarCategoria()
        {
            var caminhoJson = File.ReadAllText("C:\\Users\\Usuario\\Documents\\programacao\\alura\\csharp\\Projetos\\DesafioTaskGerenciadorSieg\\TaskManagerConsole\\bin\\Debug\\net10.0\\categoria.json");
            var categorias = JsonConvert.DeserializeObject<List<Categoria>>(caminhoJson);

            foreach (var item in categorias)
            {
                Console.WriteLine(item.Nome);
            }

            return categorias;
        }

    }

}
