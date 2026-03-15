using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Helpers;

namespace TaskManagerConsole.Repositories
{
    public class UsuarioRepository
    {
        public void CriarUsuario(Usuario usuario)
        {
            FuncoesUsuarios.EscreverArquivoUsuario(usuario);
        }

        public List<Usuario> PegarUsuarios()
        {
            List<Usuario> usuarios = FuncoesUsuarios.PegarUsuariosArquivo();
            return usuarios;
        }

    }
}
