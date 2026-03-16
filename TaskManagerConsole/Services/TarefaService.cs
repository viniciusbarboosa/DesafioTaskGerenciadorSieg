using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Entities.Types;
using TaskManagerConsole.Repositories;

namespace TaskManagerConsole.Services
{
    public class TarefaService
    {
        UsuarioRepository _usuarioRepository;
        CategoriasRepository _categoriaRepository;
        TarefasRepository _tarefasRepository;

        public TarefaService(UsuarioRepository usuarioRepository,CategoriasRepository categoriasRepository,TarefasRepository tarefasRepository)
        {
            _usuarioRepository = usuarioRepository;
            _categoriaRepository = categoriasRepository;
            _tarefasRepository = tarefasRepository;
        }

        public void CreateTarefa()
        {
            List<Categoria> categoriasVerificacao = _categoriaRepository.PegarCategorias();
            List<Usuario> usuariosVerificacao = _usuarioRepository.PegarUsuarios();

            if (categoriasVerificacao.Count == 0)
            {
                Console.WriteLine("Não é possivel Criar Tarefa sem ter pelo menos 1 categoria Criada");
                return;
            }

            if (usuariosVerificacao.Count == 0)
            {
                Console.WriteLine("Não é possivel Criar Tarefa sem ter pelo menos 1 usuario Criado");
                return;
            }

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
                    dataVencimentoString = "";
                }
            }

            while (nomeCategoria == "" || nomeCategoria == null)
            {
                Console.WriteLine("LISTAGEM DE CATEGORIAS");
                Console.WriteLine("======================================");
                List<Categoria> categorias = _categoriaRepository.PegarCategorias();


                foreach (var item in categorias.Select((x, i) => new { Value = x.Nome, index = i }))
                {
                    Console.WriteLine($" [ {item.index} ] => {item.Value}");
                }

                if (categorias.Count == 0)
                {
                    Console.WriteLine("NÃO É POSSIVEL CRIAR UMA TAREFA SEM CATEGORIAS CRIADA");
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
                List<Usuario> usuarios = _usuarioRepository.PegarUsuarios();

                if (usuarios.Count == 0)
                {
                    Console.WriteLine("NÃO É POSSIVEL CRIAR UMA TAREFA SEM USUARIOS CRIADOS");
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
            tarefa.Status = StatusTarefa.Pendente;

            _tarefasRepository.CriarTarefa(tarefa);
        }

        public void ListarTarefas()
        {
            Console.WriteLine("LISTAGEM DE TAREFAS");
            Console.WriteLine("=================================");
            List<Tarefa> tarefas = _tarefasRepository.PegarTarefas();

            foreach (var item in tarefas.Select((x, i) => new { Titulo = x.Titulo, Descricao = x.Descricao, DataVencimento = x.DataVencimento, DataCriacao = x.DataCriacao,DataConslusao = x.DataConclusao , Status = x.Status, NomeUsuario = x.NomeUsuario, NomeCategoria = x.NomeCategoria, index = i }))
            {
                if (item.Status == StatusTarefa.Concluida)
                {
                    Console.WriteLine($" Id:{item.index} = TITULO : {item.Titulo} | DESCRICAO : {item.Descricao} | DATA VENCIMENTO : {item.DataVencimento} | DATA CRIAÇÃO {item.DataCriacao} | DATA COnCLUSAO {item.DataConslusao} |STATUS : {item.Status} | CATEGORIA : {item.NomeCategoria} | RESPONSAVEL : {item.NomeUsuario}");
                    Console.WriteLine("========================================");
                }
                else
                {
                    Console.WriteLine($" Id:{item.index} = TITULO : {item.Titulo} | DESCRICAO : {item.Descricao} | DATA VENCIMENTO : {item.DataVencimento} | DATA CRIAÇÃO {item.DataCriacao} | STATUS : {item.Status} | CATEGORIA : {item.NomeCategoria} | RESPONSAVEL : {item.NomeUsuario}");
                    Console.WriteLine("========================================");
                }
                
            }

        }

        public void DeletarTarefa()
        {
            //DELETAR
            Console.WriteLine("LISTAGEM DE TAREFAS");
            Console.WriteLine("=================================");
            List<Tarefa> tarefas = _tarefasRepository.PegarTarefas();

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
            _tarefasRepository.AtualizarTarefas(tarefas);
        }

        public void UpdateTarefas()
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
            List<Tarefa> tarefas = _tarefasRepository.PegarTarefas();

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
                List<Categoria> categorias = _categoriaRepository.PegarCategorias();


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
                List<Usuario> usuarios = _usuarioRepository.PegarUsuarios();

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

            _tarefasRepository.AtualizarTarefas(tarefas);
        }

        public void ListarTarefaMarcarConcluida()
        {
            Console.WriteLine("LISTAGEM DE TAREFAS");
            Console.WriteLine("=================================");
            List<Tarefa> tarefas = _tarefasRepository.PegarTarefas();

            foreach (var item in tarefas.Select((x, i) => new { Titulo = x.Titulo, Descricao = x.Descricao, DataVencimento = x.DataVencimento, DataCriacao = x.DataCriacao, Status = x.Status, NomeUsuario = x.NomeUsuario, NomeCategoria = x.NomeCategoria, index = i }))
            {
                Console.WriteLine($" Id:{item.index} = TITULO : {item.Titulo} | DESCRICAO : {item.Descricao} | DATA VENCIMENTO : {item.DataVencimento} | DATA CRIAÇÃO {item.DataCriacao} | STATUS : {item.Status} | CATEGORIA : {item.NomeCategoria} | RESPONSAVEL : {item.NomeUsuario}");
                Console.WriteLine("========================================");
            }
            int idEscolhido;
            Console.WriteLine(" Escolha o id da Tarefa que deseja Marcar como concluída");
            idEscolhido = int.Parse(Console.ReadLine());

            Tarefa tarefaEditando = tarefas[idEscolhido];
            tarefaEditando.Status = StatusTarefa.Concluida;
            tarefaEditando.DataConclusao = DateTime.Now;

            _tarefasRepository.AtualizarTarefas(tarefas);
        }

        public void ListarTarefasOrdenadasVencimento()
        {
            //MOSTRAR ORDENADAS POR DATA DE VENCIMENTO
            Console.WriteLine("LISTAGEM DE TAREFAS POR DATA DE VENCIMENTO");
            Console.WriteLine("=================================");
            List<Tarefa> tarefas = _tarefasRepository.PegarTarefas();

            var novaLista = tarefas
                .OrderBy(i => i.DataVencimento)
                .ToList();

            foreach (var item in novaLista.Select((x, i) => new { Titulo = x.Titulo, Descricao = x.Descricao, DataVencimento = x.DataVencimento, DataCriacao = x.DataCriacao, Status = x.Status, NomeUsuario = x.NomeUsuario, NomeCategoria = x.NomeCategoria, index = i }))
            {
                Console.WriteLine($" Id:{item.index} = TITULO : {item.Titulo} | DESCRICAO : {item.Descricao} | DATA VENCIMENTO : {item.DataVencimento} | DATA CRIAÇÃO {item.DataCriacao} | STATUS : {item.Status} | CATEGORIA : {item.NomeCategoria} | RESPONSAVEL : {item.NomeUsuario}");
                Console.WriteLine("-------------------------------------");
            }
        }

        public void ListarTarefasVencidas()
        {
            //ORDENAR TAREFAS ATRASADAS
            Console.WriteLine("LISTAGEM DE TAREFAS POR DATA DE VENCIMENTO");
            Console.WriteLine("=================================");
            List<Tarefa> tarefas = _tarefasRepository.PegarTarefas();

            var novaLista = tarefas
                .Where(i => i.DataVencimento <= DateTime.Now)
                .Where(i => i.Status != StatusTarefa.Concluida)
                .ToList();

            foreach (var item in novaLista.Select((x, i) => new { Titulo = x.Titulo, Descricao = x.Descricao, DataVencimento = x.DataVencimento, DataCriacao = x.DataCriacao,DataConclusao = x.DataConclusao , Status = x.Status, NomeUsuario = x.NomeUsuario, NomeCategoria = x.NomeCategoria, index = i }))
            {
                Console.WriteLine($" Id:{item.index} = TITULO : {item.Titulo} | DESCRICAO : {item.Descricao} | DATA VENCIMENTO : {item.DataVencimento} | DATA CRIAÇÃO {item.DataCriacao} | Data Conclusao : {item.DataConclusao} |STATUS : {item.Status} | CATEGORIA : {item.NomeCategoria} | RESPONSAVEL : {item.NomeUsuario}");
                Console.WriteLine("-------------------------------------");
            }
        }

        public void FiltrarTarefasPorStatus()
        {
            List<Tarefa> tarefas = _tarefasRepository.PegarTarefas();

            Console.WriteLine($"ESCOLHA O STATUS QUE DESEJA FILTRAR");
            Console.WriteLine("[ 1 ] - Pendente");
            Console.WriteLine("[ 2 ] - Em Andamento");
            Console.WriteLine("[ 3 ] Cancelada");
            Console.WriteLine("[ 4 ] Concluida");

            int statusEscolhido;
            Console.WriteLine(" Escolha o id da Tarefa que deseja Marcar como concluída");
            while(!int.TryParse(Console.ReadLine(),out statusEscolhido))
            {
                Console.WriteLine("Apenas Permitido Numeros Inteiros");
            }

            if(statusEscolhido > 4 || statusEscolhido <= 0)
            {
                Console.WriteLine("Opção Inválida");
                return;
            }

            StatusTarefa statusTarefa = StatusTarefa.Pendente;
            if (statusEscolhido == 1)
            {
                statusTarefa = StatusTarefa.Pendente;
            }else if (statusEscolhido == 2)
            {
                statusTarefa = StatusTarefa.EmAndamento;
            }else if (statusEscolhido == 3)
            {
                statusTarefa = StatusTarefa.Cancelada;
            }else if (statusEscolhido == 4)
            {
                statusTarefa = StatusTarefa.Concluida;
            }

            var listaTarefasFiltrada = tarefas
                                       .Where(i => i.Status == statusTarefa)
                                       .ToList();

            foreach (var item in listaTarefasFiltrada.Select((x, i) => new { Titulo = x.Titulo, Descricao = x.Descricao, DataVencimento = x.DataVencimento, DataCriacao = x.DataCriacao, DataConclusao = x.DataConclusao , Status = x.Status, NomeUsuario = x.NomeUsuario, NomeCategoria = x.NomeCategoria, index = i }))
            {
                if(statusEscolhido == 4)
                {
                    Console.WriteLine($" Id:{item.index} = TITULO : {item.Titulo} | DESCRICAO : {item.Descricao} | DATA VENCIMENTO : {item.DataVencimento} | DATA CRIAÇÃO {item.DataCriacao} | DATA CONCLUSAO {item.DataConclusao}| STATUS : {item.Status} | CATEGORIA : {item.NomeCategoria} | RESPONSAVEL : {item.NomeUsuario}");
                    Console.WriteLine("-------------------------------------");
                }
                else
                {
                    Console.WriteLine($" Id:{item.index} = TITULO : {item.Titulo} | DESCRICAO : {item.Descricao} | DATA VENCIMENTO : {item.DataVencimento} | DATA CRIAÇÃO {item.DataCriacao} | STATUS : {item.Status} | CATEGORIA : {item.NomeCategoria} | RESPONSAVEL : {item.NomeUsuario}");
                    Console.WriteLine("-------------------------------------");
                }
                
            }

        }

        public void FiltrarTarefasPorCategoria()
        {
            string nomeCategoria = "";
            while (nomeCategoria == "" || nomeCategoria == null)
            {
                Console.WriteLine("LISTAGEM DE CATEGORIAS");
                Console.WriteLine("======================================");
                List<Categoria> categorias = _categoriaRepository.PegarCategorias();


                foreach (var item in categorias.Select((x, i) => new { Value = x.Nome, index = i }))
                {
                    Console.WriteLine($" [ {item.index} ] => {item.Value}");
                }

                if (categorias.Count == 0)
                {
                    Console.WriteLine("Não existe Categorias");
                    break;
                }

                Console.WriteLine($"Escolha o id Da Categoria que quer selecionar para Filtrar");
                int idCategoria;
                while (!int.TryParse(Console.ReadLine(),out idCategoria))
                {
                    Console.WriteLine($"Invalido pode apenas escolher Números");
                }

                if (idCategoria < 0 || idCategoria >= categorias.Count)
                {
                    Console.WriteLine("Não possui essa categoria");
                }
                else
                {
                    nomeCategoria = categorias[idCategoria].Nome;
                }

            }

            List<Tarefa> tarefas = _tarefasRepository.PegarTarefas();

            List<Tarefa> tarefasFiltradas;

            tarefasFiltradas = tarefas
                               .Where(i => i.NomeCategoria == nomeCategoria)
                               .ToList();

            if (tarefasFiltradas.Count == 0)
            {
                Console.WriteLine("Não possui nenhuma tarefa pra esse status");
                return;
            }

            foreach (var item in tarefasFiltradas.Select((x, i) => new { Titulo = x.Titulo, Descricao = x.Descricao, DataVencimento = x.DataVencimento, DataCriacao = x.DataCriacao, Status = x.Status, NomeUsuario = x.NomeUsuario, NomeCategoria = x.NomeCategoria, index = i }))
            {
                Console.WriteLine($" Id:{item.index} = TITULO : {item.Titulo} | DESCRICAO : {item.Descricao} | DATA VENCIMENTO : {item.DataVencimento} | DATA CRIAÇÃO {item.DataCriacao} | STATUS : {item.Status} | CATEGORIA : {item.NomeCategoria} | RESPONSAVEL : {item.NomeUsuario}");
                Console.WriteLine("-------------------------------------");
            }

        }

    }
}
