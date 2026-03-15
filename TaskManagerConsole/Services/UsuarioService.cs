using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Repositories;

namespace TaskManagerConsole.Services
{
    public class UsuarioService
    {
        UsuarioRepository _usuarioRepository = new UsuarioRepository();

        public UsuarioService(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository; 
        }
        public void CriarUsuario()
        {
            Console.WriteLine("Digite o nome do novo Usuário");
            string usuario = Console.ReadLine();
            Console.WriteLine("Digite o nome do email do novo Usuário");
            string email = Console.ReadLine();

            List<Usuario> usuarios = _usuarioRepository.PegarUsuarios();

            bool existeUsuario = false;
            foreach (var item in usuarios.Select((x, i) => new { Value = x.Nome, index = i }))
            {
                if (item.Value == usuario)
                {
                    Console.WriteLine("Não Possivel Criar Usuario Com Nome que ja existe");
                    return;
                }
            }

            Usuario usuarioNovo = new Usuario();

            usuarioNovo.Nome = usuario;
            usuarioNovo.Email = email;

            _usuarioRepository.CriarUsuario(usuarioNovo);

            Console.WriteLine("Usuário CRIADO COM SUCESSO");
        }

        public void ListarUsuarios()
        {
            Console.WriteLine("LISTAGEM DE USUÁRIOS");
            Console.WriteLine("======================================");
            var usuarios = _usuarioRepository.PegarUsuarios();
            foreach (var item in usuarios.Select((x, i) => new { Nome = x.Nome, Email = x.Email, index = i }))
            {
                Console.WriteLine($" {item.Nome} - Email : {item.Email}");
            }

        }
    }
}
