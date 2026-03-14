using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Repositories;

namespace TaskManagerConsole.Services
{
    public class TarefaService
    {
        UsuarioRepository usuarioService = new UsuarioRepository();
        CategoriasRepository categoriaService = new CategoriasRepository();
        TarefasRepisitory tarefasService = new TarefasRepisitory();

        public void createTarefa()
        {
            //CREATE TAREFA
            Console.WriteLine("CRIANDO TAREFA");
            Console.WriteLine("======================================");
            string titulo = "";
            string descricao = "";
            string dataVencimentoString = "";
            DateTime dataVencimento;
            string nomeCategoria = "";
            string nomeUsuario = "";

            while (titulo == "" || titulo == null)
            {
                Console.WriteLine("INSIRA Titulo da Tarefa ");
                titulo = Console.ReadLine();

                if (titulo == "" || titulo == null)
                {
                    Console.WriteLine("Titulo não pode ser Vazio");
                }
            }

            while (descricao == "" || descricao == null)
            {
                Console.WriteLine("INSIRA Descrição da Tarefa ");
                descricao = Console.ReadLine();

                if (descricao == "" || descricao == null)
                {
                    Console.WriteLine("Descricao não pode ser Vazio");
                }
            }

            while (dataVencimentoString == "" || dataVencimentoString == null)
            {
                try
                {
                    Console.WriteLine("Insira Data de Vencimento");
                    dataVencimentoString = Console.ReadLine();
                    dataVencimento = DateTime.Parse(dataVencimentoString);
                    Console.WriteLine(dataVencimento);
                    if (dataVencimento < DateTime.Now)
                    {
                        Console.WriteLine("Data de Vencimento não pode ser no passado");
                        dataVencimentoString = "";
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Data de Vencimento Invalida Insira outra (FORMATO ERRADO OU DATA JA PASSOU)");
                }
            }

            while (nomeCategoria == "" || nomeCategoria == null)
            {
                Console.WriteLine("LISTAGEM DE CATEGORIAS");
                Console.WriteLine("======================================");
                List<Categoria> categorias = categoriaService.pegarCategorias();


                foreach (var item in categorias.Select((x, i) => new { Value = x.Nome, index = i }))
                {
                    Console.WriteLine($" [ {item.index} ] => {item.Value}");
                }

                if (categorias.Count == 0)
                {
                    return;
                }

                Console.WriteLine("Escolha o id Da Categoria que quer selecionar");
                int idCategoria = int.Parse(Console.ReadLine());

                if (idCategoria < 0 || idCategoria >= categorias.Count)
                {
                    Console.WriteLine("Não possui essa categoria");
                }
                else
                {
                    nomeCategoria = categorias[idCategoria].Nome;
                }

            }

            while (nomeUsuario == "" || nomeUsuario == null)
            {
                Console.WriteLine("LISTAGEM DE USUARIOS");
                Console.WriteLine("======================================");
                List<Usuario> usuarios = usuarioService.PegarUsuarios();

                if (usuarios.Count == 0)
                {
                    return;
                }

                foreach (var item in usuarios.Select((x, i) => new { Value = x.Nome, index = i }))
                {
                    Console.WriteLine($" [ {item.index} ] => {item.Value}");
                }

                Console.WriteLine("Escolha o ID do Usuario que quer selecionar");
                int idUsuario = int.Parse(Console.ReadLine());

                if (idUsuario < 0 || idUsuario >= usuarios.Count)
                {
                    Console.WriteLine("Não possui essa categoria");
                }
                else
                {
                    nomeUsuario = usuarios[idUsuario].Nome;
                }


            }

            Tarefa tarefa = new Tarefa();
            tarefa.Titulo = titulo;
            tarefa.Descricao = descricao;
            tarefa.DataVencimento = DateTime.Parse(dataVencimentoString);
            tarefa.NomeCategoria = nomeCategoria;
            tarefa.NomeUsuario = nomeUsuario;
            tarefa.DataCriacao = DateTime.Now;

            tarefasService.CriarTarefa(tarefa);
        }

        public void listarTarefas()
        {
            Console.WriteLine("LISTAGEM DE TAREFAS");
            Console.WriteLine("=================================");
            tarefasService.ListarTarefa();
        }

        public void deletarTarefa()
        {
            //DELETAR
            Console.WriteLine("LISTAGEM DE TAREFAS");
            Console.WriteLine("=================================");
            List<Tarefa> tarefas = tarefasService.PegarTarefas();

            foreach (var item in tarefas.Select((x, i) => new { Titulo = x.Titulo, Descricao = x.Descricao, DataVencimento = x.DataVencimento, DataCriacao = x.DataCriacao, Status = x.Status, NomeUsuario = x.NomeUsuario, NomeCategoria = x.NomeCategoria, index = i }))
            {
                Console.WriteLine($" Id:{item.index} = TITULO : {item.Titulo} | DESCRICAO : {item.Descricao} | DATA VENCIMENTO : {item.DataVencimento} | DATA CRIAÇÃO {item.DataCriacao} | STATUS : {item.Status} | CATEGORIA : {item.NomeCategoria} | RESPONSAVEL : {item.NomeUsuario}");
                Console.WriteLine("========================================");
            }

            int idEscolhido;

            Console.WriteLine("ESCOLHA O ID DA TAREFA QUE DESEJA DELETAR");
            idEscolhido = int.Parse(Console.ReadLine());

            if (idEscolhido < 0 || idEscolhido >= tarefas.Count)
            {
                Console.WriteLine("NÃO EXISTE UMA CATEGORIA PARA O ID SELECIONADO");
                return;
            }

            tarefas.RemoveAt(idEscolhido);
            tarefasService.AtualizarTarefas(tarefas);
        }

        public void updateTarefas()
        {
            //UPDATE DE TAREFAS
            string titulo = "";
            string descricao = "";
            string dataVencimentoString = "";
            DateTime dataVencimento;
            string nomeCategoria = "";
            string nomeUsuario = "";
            string status = "";

            Console.WriteLine("LISTAGEM DE TAREFAS");
            Console.WriteLine("=================================");
            List<Tarefa> tarefas = tarefasService.PegarTarefas();

            foreach (var item in tarefas.Select((x, i) => new { Titulo = x.Titulo, Descricao = x.Descricao, DataVencimento = x.DataVencimento, DataCriacao = x.DataCriacao, Status = x.Status, NomeUsuario = x.NomeUsuario, NomeCategoria = x.NomeCategoria, index = i }))
            {
                Console.WriteLine($" Id:{item.index} = TITULO : {item.Titulo} | DESCRICAO : {item.Descricao} | DATA VENCIMENTO : {item.DataVencimento} | DATA CRIAÇÃO {item.DataCriacao} | STATUS : {item.Status} | CATEGORIA : {item.NomeCategoria} | RESPONSAVEL : {item.NomeUsuario}");
                Console.WriteLine("========================================");
            }

            int idEscolhido;
            Console.WriteLine("ESCOLHA O ID DA TAREFA QUE DESEJA EDITAR");
            idEscolhido = int.Parse(Console.ReadLine());

            if (idEscolhido < 0 || idEscolhido >= tarefas.Count)
            {
                Console.WriteLine("ID INVALIDO");
                return;
            }

            Tarefa tarefaEditando = tarefas[idEscolhido];
            Console.Clear();

            while (status == "" || status == null)
            {
                Console.WriteLine($"ESCOLHA O STATUS [Atual é {tarefaEditando.Status}]");
                Console.WriteLine("[ 1 ] - Pendente");
                Console.WriteLine("[ 2 ] - Em Andamento");
                Console.WriteLine("[ 3 ] Cancelada");

                string escolhaStatus = Console.ReadLine();

                if (escolhaStatus == "1")
                {
                    status = "Pendente";
                }
                else if (escolhaStatus == "2")
                {
                    status = "Em Andamento";
                }
                else if (escolhaStatus == "3")
                {
                    status = "Cancelada";
                }
                else
                {
                    Console.WriteLine("Escolha Invalida escolha outra");
                }
            }


            while (titulo == "" || titulo == null)
            {
                Console.WriteLine($"Qual novo Titulo que deseja colocar [Atual : {tarefaEditando.Titulo}]");
                titulo = Console.ReadLine();

                if (titulo == "")
                {
                    Console.WriteLine("TitulO NÃO pode ser vazio");
                }

            }

            while (descricao == "" || descricao == null)
            {
                Console.WriteLine($"Qual nova Descrição que deseja colocar [Atual : {tarefaEditando.Descricao}]");
                descricao = Console.ReadLine();

                if (descricao == "")
                {
                    Console.WriteLine("Descricao NÃO pode ser vazio");
                }

            }

            while (dataVencimentoString == "" || dataVencimentoString == null)
            {
                try
                {
                    Console.WriteLine($"Qual a nova Data de Vencimento que deseja [Atual: {tarefaEditando.DataVencimento}]");
                    dataVencimentoString = Console.ReadLine();
                    dataVencimento = DateTime.Parse(dataVencimentoString);
                    Console.WriteLine(dataVencimento);
                    if (dataVencimento < DateTime.Now)
                    {
                        Console.WriteLine("Data de Vencimento não pode ser no passado");
                        dataVencimentoString = "";
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Data de Vencimento Invalida Insira outra (FORMATO ERRADO OU DATA JA PASSOU)");
                }
            }

            while (nomeCategoria == "" || nomeCategoria == null)
            {
                Console.WriteLine("LISTAGEM DE CATEGORIAS");
                Console.WriteLine("======================================");
                List<Categoria> categorias = categoriaService.pegarCategorias();


                foreach (var item in categorias.Select((x, i) => new { Value = x.Nome, index = i }))
                {
                    Console.WriteLine($" [ {item.index} ] => {item.Value}");
                }

                if (categorias.Count == 0)
                {
                    Console.WriteLine("Não existe Categorias");
                    break;
                }

                Console.WriteLine($"Escolha o id Da Categoria que quer selecionar [A atual é {tarefaEditando.NomeCategoria}]");
                int idCategoria = int.Parse(Console.ReadLine());

                if (idCategoria < 0 || idCategoria >= categorias.Count)
                {
                    Console.WriteLine("Não possui essa categoria");
                }
                else
                {
                    nomeCategoria = categorias[idCategoria].Nome;
                }

            }

            while (nomeUsuario == "" || nomeUsuario == null)
            {
                Console.WriteLine("LISTAGEM DE USUARIOS");
                Console.WriteLine("======================================");
                List<Usuario> usuarios = usuarioService.PegarUsuarios();

                if (usuarios.Count == 0)
                {
                    break;
                }

                foreach (var item in usuarios.Select((x, i) => new { Value = x.Nome, index = i }))
                {
                    Console.WriteLine($" [ {item.index} ] => {item.Value}");
                }

                Console.WriteLine($"Escolha o ID do Usuario que quer selecionar [A atual é {tarefaEditando.NomeUsuario}]");
                int idUsuario = int.Parse(Console.ReadLine());

                if (idUsuario < 0 || idUsuario >= usuarios.Count)
                {
                    Console.WriteLine("Não possui essa categoria");
                }
                else
                {
                    nomeUsuario = usuarios[idUsuario].Nome;
                }

            }

            tarefaEditando.Titulo = titulo;
            tarefaEditando.Descricao = descricao;
            tarefaEditando.DataVencimento = DateTime.Parse(dataVencimentoString);
            tarefaEditando.NomeCategoria = nomeCategoria;
            tarefaEditando.NomeUsuario = nomeUsuario;
            tarefaEditando.DataCriacao = DateTime.Now;

            tarefasService.AtualizarTarefas(tarefas);
        }

        public void listarTarefaConcluidas()
        {
            Console.WriteLine("LISTAGEM DE TAREFAS");
            Console.WriteLine("=================================");
            List<Tarefa> tarefas = tarefasService.PegarTarefas();

            foreach (var item in tarefas.Select((x, i) => new { Titulo = x.Titulo, Descricao = x.Descricao, DataVencimento = x.DataVencimento, DataCriacao = x.DataCriacao, Status = x.Status, NomeUsuario = x.NomeUsuario, NomeCategoria = x.NomeCategoria, index = i }))
            {
                Console.WriteLine($" Id:{item.index} = TITULO : {item.Titulo} | DESCRICAO : {item.Descricao} | DATA VENCIMENTO : {item.DataVencimento} | DATA CRIAÇÃO {item.DataCriacao} | STATUS : {item.Status} | CATEGORIA : {item.NomeCategoria} | RESPONSAVEL : {item.NomeUsuario}");
                Console.WriteLine("========================================");
            }
            int idEscolhido;
            Console.WriteLine(" Escolha o id da Tarefa que deseja Marcar como concluída");
            idEscolhido = int.Parse(Console.ReadLine());

            Tarefa tarefaEditando = tarefas[idEscolhido];
            tarefaEditando.Status = "Concluída";

            tarefasService.AtualizarTarefas(tarefas);
        }

        public void listarTarefasOrdenadasVencimento()
        {
            //MOSTRAR ORDENADAS POR DATA DE VENCIMENTO
            Console.WriteLine("LISTAGEM DE TAREFAS POR DATA DE VENCIMENTO");
            Console.WriteLine("=================================");
            List<Tarefa> tarefas = tarefasService.PegarTarefas();

            var novaLista = tarefas
                .OrderBy(i => i.DataVencimento)
                .ToList();

            foreach (var item in novaLista.Select((x, i) => new { Titulo = x.Titulo, Descricao = x.Descricao, DataVencimento = x.DataVencimento, DataCriacao = x.DataCriacao, Status = x.Status, NomeUsuario = x.NomeUsuario, NomeCategoria = x.NomeCategoria, index = i }))
            {
                Console.WriteLine($" Id:{item.index} = TITULO : {item.Titulo} | DESCRICAO : {item.Descricao} | DATA VENCIMENTO : {item.DataVencimento} | DATA CRIAÇÃO {item.DataCriacao} | STATUS : {item.Status} | CATEGORIA : {item.NomeCategoria} | RESPONSAVEL : {item.NomeUsuario}");
                Console.WriteLine("-------------------------------------");
            }
        }

        public void listarTarefasVencidas()
        {
            //ORDENAR TAREFAS ATRASADAS
            Console.WriteLine("LISTAGEM DE TAREFAS POR DATA DE VENCIMENTO");
            Console.WriteLine("=================================");
            List<Tarefa> tarefas = tarefasService.PegarTarefas();

            var novaLista = tarefas
                .Where(i => i.DataVencimento <= DateTime.Now)
                .Where(i => i.Status != "Concluída")
                .ToList();

            foreach (var item in novaLista.Select((x, i) => new { Titulo = x.Titulo, Descricao = x.Descricao, DataVencimento = x.DataVencimento, DataCriacao = x.DataCriacao, Status = x.Status, NomeUsuario = x.NomeUsuario, NomeCategoria = x.NomeCategoria, index = i }))
            {
                Console.WriteLine($" Id:{item.index} = TITULO : {item.Titulo} | DESCRICAO : {item.Descricao} | DATA VENCIMENTO : {item.DataVencimento} | DATA CRIAÇÃO {item.DataCriacao} | STATUS : {item.Status} | CATEGORIA : {item.NomeCategoria} | RESPONSAVEL : {item.NomeUsuario}");
                Console.WriteLine("-------------------------------------");
            }
        }
    }
}
