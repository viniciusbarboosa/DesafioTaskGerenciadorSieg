using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;

namespace TaskManagerConsole.Helpers
{
    public class FuncoesCategorias
    {
        public static string CaminhoArquivoCategorias()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json");
            return path;
        }

        public static void VerificarArquivoCategorias()
        {
            var path = CaminhoArquivoCategorias();

            if (!File.Exists(path))
            {
                File.WriteAllText(path, "[]");
            }
        }

        public static void EscreverArquivoCategorias(Categoria categoria)
        {
            VerificarArquivoCategorias();

            var caminhoJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json"));
            var categorias = JsonConvert.DeserializeObject<List<Categoria>>(caminhoJson);
            categorias.Add(categoria);
            var tarefasString = JsonConvert.SerializeObject(categorias);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json");
            File.WriteAllText(path, tarefasString);
        }

        public static void AtualizarArquvioCategorias(List<Categoria> listaCategorias)
        {
            VerificarArquivoCategorias();

            var caminhoJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json"));
            var categoriasString = JsonConvert.SerializeObject(listaCategorias);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json");
            File.WriteAllText(path, categoriasString);
        }

        public static List<Categoria> PegarCategoriasArquivo()
        {
            VerificarArquivoCategorias();

            var caminhoJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "categoria.json"));
            var categorias = JsonConvert.DeserializeObject<List<Categoria>>(caminhoJson);

            return categorias;
        }
    }
}
