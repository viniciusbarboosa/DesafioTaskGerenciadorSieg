using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Repositories;

namespace TaskManagerConsole.Services
{
    public class UsuarioService
    {
        UsuarioRepository usuarioService = new UsuarioRepository();
        CategoriasRepository categoriaService = new CategoriasRepository();
        TarefasRepisitory tarefasService = new TarefasRepisitory();

        public void CriarUsuario()
        {
            Console.WriteLine("Digite o nome do novo Usuário");
            string usuario = Console.ReadLine();
            Console.WriteLine("Digite o nome do email do novo Usuário");
            string email = Console.ReadLine();

            List<Usuario> usuarios = usuarioService.pegarUsuarios();

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

            usuarioService.CriarUsuario(usuarioNovo);

            Console.WriteLine("Usuário CRIADO COM SUCESSO");
        }

        public void ListarUsuarios()
        {
            Console.WriteLine("LISTAGEM DE USUÁRIOS");
            Console.WriteLine("======================================");
            usuarioService.ListarUsuario();
        }
    }
}
