using TaskManagerConsole.Entities;
using TaskManagerConsole.Repositories;
using TaskManagerConsole.Services;
using TaskManagerConsole.Views;

class Program{

    public static void Main(string[] args)
    {
        bool continueApplication = true;

        UserRepository userRepository = new UserRepository();
        CategoryRepository categoryRepository = new CategoryRepository();
        TaskRepository taskRepository = new TaskRepository();

        UserService userServices = new UserService(userRepository);
        CategoryService categoryServices = new CategoryService(categoryRepository, taskRepository);
        TaskService taskServices = new TaskService(userRepository, categoryRepository, taskRepository);

        
        while (continueApplication) {

            int option = 99;

            Menu.ShowMenu();
            while(!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Digite um Valor Valido (Apenas Numeros Inteiros)");
            }

            switch (option)
            {
                case 1:
                    userServices.CreateUser();
                    break;
                case 2:
                    userServices.GetUsers();
                    break;
                case 3:
                    categoryServices.CreateCategory();
                    break;
                case 4:
                    categoryServices.GetCategory();
                    break;
                case 5:
                    categoryServices.DeleteCategory();
                    break;
                case 6:
                    taskServices.CreateTask();
                    break;
                case 7:
                    taskServices.GetTask();
                    break;
                case 8:
                    taskServices.DeleteTask();
                    break;
                case 9:
                    taskServices.UpdateTask();
                    break;
                case 10:
                    taskServices.GetTaskToMarkAsComplete();
                    break;
                case 11:
                    taskServices.ListTasksSortedDueDate();
                    break;
                case 12:
                    taskServices.ListOverdueTasks();
                    break;
                case 13:
                    taskServices.FilterTasksByStatus();
                    break;
                case 14:
                    taskServices.FilterTasksByCategory();
                    break;
                case 15:
                    continueApplication = false;
                    break;
                default:
                    Console.WriteLine("Opção escolhida Invalida");
                    break;
            }

            string choice;
            if (continueApplication == true)
            {
                Console.WriteLine("Deseja Parar a aplicação ? Caso queira digite 'S' ");
                choice = Console.ReadLine();
            }
            else
            {
                choice = "S";
            }
            

            if(choice.ToUpper() == "S")
            {
                continueApplication = false;
            }
            
        }


    }
}