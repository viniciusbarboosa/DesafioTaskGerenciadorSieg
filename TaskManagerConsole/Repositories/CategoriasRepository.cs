using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Helpers;

namespace TaskManagerConsole.Repositories
{
    public class CategoriasRepository
    {

        public void CriarCategoria(Categoria categoria)
        {
            FuncoesCategorias.EscreverArquivoCategorias(categoria);
        }

        public void AtualizarCategoria(List<Categoria> listaCategorias)
        {
            FuncoesCategorias.AtualizarArquvioCategorias(listaCategorias);
        }

        public List<Categoria> PegarCategorias()
        {
            var categorias = FuncoesCategorias.PegarCategoriasArquivo();
            return categorias;
        }

    }

}
