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
            var verificarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json"); ;
            if (!File.Exists(verificarPath))
            {
                File.WriteAllText(verificarPath, "[]");
            }

            var caminhoJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json"));
            var categorias = JsonConvert.DeserializeObject<List<Categoria>>(caminhoJson);
            categorias.Add(categoria);
            var categoriasString = JsonConvert.SerializeObject(categorias);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json");
            File.WriteAllText(path, categoriasString);
        }

        public void AtualizarCategoria(List<Categoria> listaTarefas)
        {
            var verificarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json"); ;
            if (!File.Exists(verificarPath))
            {
                File.WriteAllText(verificarPath, "[]");
            }

            var caminhoJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json"));
            var tarefasString = JsonConvert.SerializeObject(listaTarefas);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json");
            File.WriteAllText(path, tarefasString);
        }

        public List<Categoria> ListarCategoria()
        {
            var verificarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json"); ;
            if (!File.Exists(verificarPath))
            {
                File.WriteAllText(verificarPath, "[]");
            }

            var caminhoJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json"));
            var categorias = JsonConvert.DeserializeObject<List<Categoria>>(caminhoJson);

            foreach (var item in categorias)
            {
                Console.WriteLine(item.Nome);
            }

            return categorias;
        }

        public List<Categoria> pegarCategorias()
        {
            var verificarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json"); ;
            if (!File.Exists(verificarPath))
            {
                File.WriteAllText(verificarPath, "[]");
            }

            var caminhoJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json"));
            var categorias = JsonConvert.DeserializeObject<List<Categoria>>(caminhoJson);
            return categorias;
        }

    }

}
