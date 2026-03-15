using TaskManagerConsole.Entities;
using TaskManagerConsole.Repositories;
using TaskManagerConsole.Services;
using TaskManagerConsole.Views;

class Program{

    public static void Main(string[] args)
    {
        bool continuarAplicacao = true;

        UsuarioRepository usuarioRepository = new UsuarioRepository();
        CategoriasRepository categoriasRepository = new CategoriasRepository();
        TarefasRepository tarefasRepository = new TarefasRepository();

        UsuarioService usuarioServices = new UsuarioService(usuarioRepository);
        CategoriaService categoriaServices = new CategoriaService(categoriasRepository,tarefasRepository);
        TarefaService tarefaServices = new TarefaService(usuarioRepository,categoriasRepository,tarefasRepository);

        
        while (continuarAplicacao) {

            int opcao = 99;

            Menu.MostrarMenu();

            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    usuarioServices.CriarUsuario();
                    break;
                case 2:
                    usuarioServices.ListarUsuarios();
                    break;
                case 3:
                    categoriaServices.CriarCategoria();
                    break;
                case 4:
                    categoriaServices.ListarCategorias();
                    break;
                case 5:
                    categoriaServices.DeletarCategoria();
                    break;
                case 6:
                    tarefaServices.CreateTarefa();
                    break;
                case 7:
                    tarefaServices.ListarTarefas();
                    break;
                case 8:
                    tarefaServices.DeletarTarefa();
                    break;
                case 9:
                    tarefaServices.UpdateTarefas();
                    break;
                case 10:
                    tarefaServices.ListarTarefaConcluidas();
                    break;
                case 11:
                    tarefaServices.ListarTarefasOrdenadasVencimento();
                    break;
                case 12:
                    tarefaServices.ListarTarefasVencidas();
                    break;
                case 13:
                    continuarAplicacao = false;
                    break;
                default:
                    Console.WriteLine("Opção escolhida Invalida");
                    break;
            }

            string escolha;
            if (continuarAplicacao == true)
            {
                Console.WriteLine("Deseja Parar a aplicação ? Caso queira digite 'S' ");
                escolha = Console.ReadLine();
            }
            else
            {
                escolha = "S";
            }
            

            if(escolha.ToUpper() == "S")
            {
                continuarAplicacao = false;
            }
            
        }


    }
}