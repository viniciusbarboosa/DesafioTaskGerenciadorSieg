using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;

namespace TaskManagerConsole.Services
{
    public class UsuarioService
    {
        public void CriarUsuario(Usuario usuario)
        {
            var caminhoJson = File.ReadAllText("C:\\Users\\Sieg\\Documents\\alura\\TaskManagerConsole\\TaskManagerConsole\\bin\\Debug\\net10.0\\usuario.json");
            var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(caminhoJson);
            usuarios.Add(usuario);
            var usuariosString = JsonConvert.SerializeObject(usuarios);

            var path = Path.Combine("C:\\Users\\Sieg\\Documents\\alura\\TaskManagerConsole\\TaskManagerConsole\\bin\\Debug\\net10.0\\usuario.json");
            File.WriteAllText(path, usuariosString);

        }

        public List<Usuario> ListarUsuario()
        {
            var caminhoJson = File.ReadAllText("C:\\Users\\Sieg\\Documents\\alura\\TaskManagerConsole\\TaskManagerConsole\\bin\\Debug\\net10.0\\usuario.json");
            var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(caminhoJson);
            
            foreach (var item in usuarios)
            {
                Console.WriteLine(item.Nome);
            }

            return usuarios;

        }

    }
}
