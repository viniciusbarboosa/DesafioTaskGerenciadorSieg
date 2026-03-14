using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;

namespace TaskManagerConsole.Repositories
{
    public class UsuarioRepository
    {
        public void CriarUsuario(Usuario usuario)
        {
            var verificarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuario.json"); ;
            if (!File.Exists(verificarPath))
            {
                File.WriteAllText(verificarPath, "[]");
            }

            var caminhoJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuario.json"));
            var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(caminhoJson);
            usuarios.Add(usuario);
            var usuariosString = JsonConvert.SerializeObject(usuarios);

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuario.json");
            File.WriteAllText(path, usuariosString);

        }

        public List<Usuario> ListarUsuario()
        {

            var verificarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuario.json"); ;
            if (!File.Exists(verificarPath))
            {
                File.WriteAllText(verificarPath, "[]");
            }
            
            var caminhoJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuario.json"));
            var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(caminhoJson);
            
            foreach (var item in usuarios)
            {
                Console.WriteLine(item.Nome);
            }

            return usuarios;

        }

        public List<Usuario> PegarUsuarios()
        {
            var verificarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuario.json"); ;
            if (!File.Exists(verificarPath))
            {
                File.WriteAllText(verificarPath, "[]");
            }

            var caminhoJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuario.json"));
            var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(caminhoJson);
            return usuarios;

        }

    }
}
