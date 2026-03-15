using TaskManagerConsole.Entities;
using TaskManagerConsole.Repositories;
using TaskManagerConsole.Services;
using TaskManagerConsole.Views;

class Program{

    public static void Main(string[] args)
    {
        bool continuarAplicacao = true;

        UsuarioService usuarioServices = new UsuarioService();
        CategoriaService categoriaServices = new CategoriaService();
        TarefaService tarefaServices = new TarefaService();

        
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
                    categoriaServices.criarCategoria();
                    break;
                case 4:
                    categoriaServices.listarCategorias();
                    break;
                case 5:
                    categoriaServices.deletarCategoria();
                    break;
                case 6:
                    tarefaServices.createTarefa();
                    break;
                case 7:
                    tarefaServices.listarTarefas();
                    break;
                case 8:
                    tarefaServices.deletarTarefa();
                    break;
                case 


            }

               
            

            if (opcao == 1)
            {
                usuarioServices.CriarUsuario();
            }
            
            else if (opcao == 4)
            {
                
            }
            else if (opcao == 5)
            {
                
            }
            else if (opcao == 6)
            {
                
            }
            else if (opcao == 7)
            {
                
            }
            else if (opcao == 8)
            {
                
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