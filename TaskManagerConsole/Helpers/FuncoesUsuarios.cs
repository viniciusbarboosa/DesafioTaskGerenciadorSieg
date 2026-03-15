using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;

namespace TaskManagerConsole.Helpers
{
    public class FuncoesUsuarios
    {

        public static string CaminhoArquivoUsuario()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuario.json"); ;
            return path;
        }

        public static void VerificarArquivoUsuario()
        {
            var path = CaminhoArquivoUsuario();

            if (!File.Exists(path))
            {
                File.WriteAllText(path, "[]");
            }
        }

        public static void EscreverArquivoUsuario(Usuario usuario)
        {
            VerificarArquivoUsuario();

            var caminhoJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuario.json"));
            var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(caminhoJson);
            usuarios.Add(usuario);
            var usuariosString = JsonConvert.SerializeObject(usuarios);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuario.json");
            File.WriteAllText(path, usuariosString);
        }

        public static List<Usuario> PegarUsuariosArquivo()
        {
            VerificarArquivoUsuario();

            var caminhoJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuario.json"));
            var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(caminhoJson);

            return usuarios;
        }

    }
}
