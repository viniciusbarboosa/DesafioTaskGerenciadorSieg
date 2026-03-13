using TaskManagerConsole.Entities;
using TaskManagerConsole.Services;

class Program{

    public static void Main(string[] args)
    {
        bool continuarAplicacao = true;
        UsuarioService usuarioService = new UsuarioService();
        CategoriasService categoriaService = new CategoriasService();

        
        while (continuarAplicacao) {

            int opcao = 99;

            Console.Clear();


            Console.WriteLine("[1] Criar Um Usuário");
            Console.WriteLine("[2] Listar Usuarios");
            Console.WriteLine("[3] Criar Uma Categoria");
            Console.WriteLine("[4] Listar Categorias");
            Console.WriteLine("[5] Criando Tarefas");
            Console.WriteLine("[6]  Sair");


            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                Console.WriteLine("Digite o nome do novo Usuário");
                string usuario = Console.ReadLine();
                Console.WriteLine("Digite o nome do email do novo Usuário");
                string email = Console.ReadLine();

                Usuario usuarioNovo = new Usuario();

                usuarioNovo.Nome = usuario;
                usuarioNovo.Email = email;

                usuarioService.CriarUsuario(usuarioNovo);

                Console.WriteLine("Usuário CRIADO COM SUCESSO");

            }
            else if (opcao == 2)
            {
                Console.WriteLine("LISTAGEM DE USUÁRIOS");
                Console.WriteLine("======================================");
                usuarioService.ListarUsuario();
            }
            else  if(opcao == 3)
            {
                Console.WriteLine("Digite o nome da Categoria");
                string nomeCategoria = Console.ReadLine();
                Console.WriteLine("Digite a cor da Categoria");
                string cor = Console.ReadLine();

                Categoria categoria = new Categoria();
                categoria.Nome = nomeCategoria;
                categoria.Cor = cor;

                categoriaService.CriarCategoria(categoria);
            }
            else if (opcao == 4)
            {
                Console.WriteLine("LISTAGEM DE CATEGORIAS");
                Console.WriteLine("======================================");
                categoriaService.ListarCategoria();
            }else if (opcao == 5)
            {
                Console.WriteLine("CRIANDO TAREFA");
                Console.WriteLine("======================================");
                string titulo = "";
                string descricao = "";
                string dataVencimentoString = "";
                DateTime dataVencimento;
                string nomeCategoria = "";

                while(titulo == "" || titulo == null)
                {
                    Console.WriteLine("INSIRA Titulo da Tarefa ");
                    titulo = Console.ReadLine();
                    
                    if(titulo == "" || titulo == null)
                    {
                        Console.WriteLine("Titulo não pode ser Vazio");
                    }
                }

                while(descricao == "" || descricao == null)
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
                        if(dataVencimento < DateTime.Now)
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
                    List<Categoria> categorias = categoriaService.ListarCategoria();
                    
                    foreach (var item in categorias.Select((x,i)=> new { Value = x,index = i}) )
                    {
                        Console.WriteLine($" [ {item.index} ] => {item.Value}");
                    }

                    if (categorias.Count == 0)
                    {
                        break;
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



            }
            else if (opcao == 6)
            {
                break;
            }
            else
            {
                Console.WriteLine("Opção escolhida Invalida");
            }

            
            Console.WriteLine("Deseja Parar a aplicação ? Caso queira digite 'S' ");
            string escolha = Console.ReadLine();

            if(escolha.ToUpper() == "S")
            {
                continuarAplicacao = false;
            }
            
        }


    }
}