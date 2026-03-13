using TaskManagerConsole.Entities;
using TaskManagerConsole.Repositories;
using TaskManagerConsole.Services;

class Program{

    public static void Main(string[] args)
    {
        bool continuarAplicacao = true;

        UsuarioService usuarioServices = new UsuarioService();
        CategoriaService categoriaServices = new CategoriaService();
        TarefaService tarefaServices = new TarefaService();

        
        while (continuarAplicacao) {

            int opcao = 99;

            Console.Clear();


            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[1] Criar Um Usuário");
            Console.WriteLine("[2] Listar Usuarios");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[3] Criar Uma Categoria");
            Console.WriteLine("[4] Listar Categorias");
            Console.WriteLine("[5] Deletar Categorias");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[6] Criando Tarefas");
            Console.WriteLine("[7] Listar Tarefas");
            Console.WriteLine("[8] Deletar Tarefas");
            Console.WriteLine("[9] Editar Tarefas");
            Console.WriteLine("[10] Marcar Tarefas Como Concluída");
            Console.WriteLine("[11] Listar Tarefas Ordernadas Por Data de Vencimento");
            Console.WriteLine("[12] Listar Tarefas Venceu");
            Console.ResetColor();
            
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("[13]  Sair");
            Console.ResetColor();


            opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                usuarioServices.CriarUsuario();
            }
            else if (opcao == 2)
            {
                usuarioServices.ListarUsuarios();
            }
            else if (opcao == 3)
            {
                categoriaServices.criarCategoria();
            }
            else if (opcao == 4)
            {
                categoriaServices.listarCategorias();
            }
            else if (opcao == 5)
            {
                categoriaServices.deletarCategoria();
            }
            else if (opcao == 6)
            {
                tarefaServices.createTarefa();
            }
            else if (opcao == 7)
            {
                tarefaServices.listarTarefas();
            }
            else if (opcao == 8)
            {
                tarefaServices.deletarTarefa();
            }
            else if (opcao == 9)
            {
                tarefaServices.updateTarefas();
            }
            else if (opcao == 10)
            {
                tarefaServices.listarTarefaConcluidas();
            }
            else if (opcao == 11)
            {
                tarefaServices.listarTarefasOrdenadasVencimento();
            }
            else if (opcao == 12)
            {
                tarefaServices.listarTarefasVencidas();
            }
            else if (opcao == 13)
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