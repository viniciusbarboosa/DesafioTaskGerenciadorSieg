using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Entities.Types;
using TaskManagerConsole.Repositories;
using TaskManagerConsole.Repositories.interfaces;

namespace TaskManagerConsole.Services
{
    public class TaskService
    {
        IRepository<User> _userRepository;
        IRepository<Category> _categoryRepository;
        IRepository<Tasks> _taskRepository;

        public TaskService(IRepository<User> userRepository, IRepository<Category> categoryRepository, IRepository<Tasks> taskRepository)
        {
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _taskRepository = taskRepository;
        }

        public void CreateTask()
        {
            List<Category> categoriesVerification = _categoryRepository.Get();
            List<User> usersVerification = _userRepository.Get();

            if (categoriesVerification.Count == 0)
            {
                Console.WriteLine("Não é possivel Criar Tarefa sem ter pelo menos 1 categoria Criada");
                return;
            }

            if (usersVerification.Count == 0)
            {
                Console.WriteLine("Não é possivel Criar Tarefa sem ter pelo menos 1 usuario Criado");
                return;
            }

            //CREATE TAREFA
            Console.WriteLine("CRIANDO TAREFA");
            Console.WriteLine("======================================");
            string title = "";
            string description = "";
            string dueDateString = "";
            DateTime dueDate;
            string categoryName = "";
            string userName = "";

            while (title == "" || title == null)
            {
                Console.WriteLine("INSIRA Titulo da Tarefa ");
                title = Console.ReadLine();

                if (title == "" || title == null)
                {
                    Console.WriteLine("Titulo não pode ser Vazio");
                }
            }

            while (description == "" || description == null)
            {
                Console.WriteLine("INSIRA Descrição da Tarefa ");
                description = Console.ReadLine();

                if (description == "" || description == null)
                {
                    Console.WriteLine("Descricao não pode ser Vazio");
                }
            }

            while (dueDateString == "" || dueDateString == null)
            {
                try
                {
                    Console.WriteLine("Insira Data de Vencimento");
                    dueDateString = Console.ReadLine();
                    dueDate = DateTime.Parse(dueDateString);
                    Console.WriteLine(dueDate);
                    if (dueDate < DateTime.Now)
                    {
                        Console.WriteLine("Data de Vencimento não pode ser no passado");
                        dueDateString = "";
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Data de Vencimento Invalida Insira outra (FORMATO ERRADO OU DATA JA PASSOU)");
                    dueDateString = "";
                }
            }

            while (categoryName == "" || categoryName == null)
            {
                Console.WriteLine("LISTAGEM DE CATEGORIAS");
                Console.WriteLine("======================================");
                List<Category> categories = _categoryRepository.Get();


                foreach (var item in categories.Select((x, i) => new { Value = x.Name, index = i }))
                {
                    Console.WriteLine($" [ {item.index} ] => {item.Value}");
                }

                if (categories.Count == 0)
                {
                    Console.WriteLine("NÃO É POSSIVEL CRIAR UMA TAREFA SEM CATEGORIAS CRIADA");
                    return;
                }

                Console.WriteLine("Escolha o id Da Categoria que quer selecionar");
                int idCategory;
                while (!int.TryParse(Console.ReadLine(),out idCategory))
                {
                    Console.WriteLine("Id categoria Invalida insira novamente um numero valido");
                }

                if (idCategory < 0 || idCategory >= categories.Count)
                {
                    Console.WriteLine("Não possui essa categoria");
                }
                else
                {
                    categoryName = categories[idCategory].Name;
                }

            }

            while (userName == "" || userName == null)
            {
                Console.WriteLine("LISTAGEM DE USUARIOS");
                Console.WriteLine("======================================");
                List<User> users = _userRepository.Get();

                if (users.Count == 0)
                {
                    Console.WriteLine("NÃO É POSSIVEL CRIAR UMA TAREFA SEM USUARIOS CRIADOS");
                    return;
                }

                foreach (var item in users.Select((x, i) => new { Value = x.Name, index = i }))
                {
                    Console.WriteLine($" [ {item.index} ] => {item.Value}");
                }

                Console.WriteLine("Escolha o ID do Usuario que quer selecionar");
                
                int idUser;
                while(!int.TryParse(Console.ReadLine(),out idUser))
                {
                    Console.WriteLine("O id precisa ser um numero inteiro.Escreva um váçlido");
                }

                if (idUser < 0 || idUser >= users.Count)
                {
                    Console.WriteLine("Não possui essa categoria");
                }
                else
                {
                    userName = users[idUser].Name;
                }


            }

            Tasks task = new Tasks(title,description, DateTime.Parse(dueDateString),categoryName,userName,StatusTask.Pendente);
            _taskRepository.Create(task);
        }

        public void GetTask()
        {
            Console.WriteLine("LISTAGEM DE TAREFAS");
            Console.WriteLine("=================================");
            List<Tasks> tasks = _taskRepository.Get();

            foreach (var item in tasks.Select((x, i) => new { Title = x.Title , Description = x.Description, DateDue = x.DateDue, DateCreation = x.DateCreation , DateCompletion = x.DateCompletion, Status = x.Status, NameUser = x.NameUser, NameCategory = x.NameCategory, index = i }))
            {
                if (item.Status == StatusTask.Concluida)
                {
                    Console.WriteLine($" Id:{item.index} = TITULO : {item.Title} | DESCRICAO : {item.Description} | DATA VENCIMENTO : {item.DateDue} | DATA CRIAÇÃO {item.DateCreation} | DATA COnCLUSAO {item.DateCompletion} |STATUS : {item.Status} | CATEGORIA : {item.NameCategory} | RESPONSAVEL : {item.NameUser}");
                    Console.WriteLine("========================================");
                }
                else
                {
                    Console.WriteLine($" Id:{item.index} = TITULO : {item.Title} | DESCRICAO : {item.Description} | DATA VENCIMENTO : {item.DateDue} | DATA CRIAÇÃO {item.DateCreation} | STATUS : {item.Status} | CATEGORIA : {item.NameCategory} | RESPONSAVEL : {item.NameUser}");
                    Console.WriteLine("========================================");
                }
                
            }

        }

        public void DeleteTask()
        {
            //DELETAR
            Console.WriteLine("LISTAGEM DE TAREFAS");
            Console.WriteLine("=================================");
            List<Tasks> tasks = _taskRepository.Get();

            foreach (var item in tasks.Select((x, i) => new { Title = x.Title, Description = x.Description, DateDue = x.DateDue, DateCreation = x.DateCreation, DateCompletion = x.DateCompletion, Status = x.Status, NameUser = x.NameUser, NameCategory = x.NameCategory, index = i }))
            {
                if (item.Status == StatusTask.Concluida)
                {
                    Console.WriteLine($" Id: [ {item.index} ] = TITULO : {item.Title} | DESCRICAO : {item.Description} | DATA VENCIMENTO : {item.DateDue} | DATA CRIAÇÃO {item.DateCreation} | DATA COnCLUSAO {item.DateCompletion} |STATUS : {item.Status} | CATEGORIA : {item.NameCategory} | RESPONSAVEL : {item.NameUser}");
                    Console.WriteLine("========================================");
                }
                else
                {
                    Console.WriteLine($" Id: [ {item.index} ] = TITULO : {item.Title} | DESCRICAO : {item.Description} | DATA VENCIMENTO : {item.DateDue} | DATA CRIAÇÃO {item.DateCreation} | STATUS : {item.Status} | CATEGORIA : {item.NameCategory} | RESPONSAVEL : {item.NameUser}");
                    Console.WriteLine("========================================");
                }

            }

            int idChosen;

            Console.WriteLine("ESCOLHA O ID DA TAREFA QUE DESEJA DELETAR");
            idChosen = int.Parse(Console.ReadLine());

            if (idChosen < 0 || idChosen >= tasks.Count)
            {
                Console.WriteLine("NÃO EXISTE UMA CATEGORIA PARA O ID SELECIONADO");
                return;
            }

            tasks.RemoveAt(idChosen);
            _taskRepository.Update(tasks);
        }

        public void UpdateTask()
        {
            //UPDATE DE TAREFAS
            string title = "";
            string description = "";
            string dueDateString = "";
            DateTime dueDate;
            string nameCategory = "";
            string nameUser = "";
            string status = "";
            StatusTask statusTask = StatusTask.Pendente; 

            Console.WriteLine("LISTAGEM DE TAREFAS");
            Console.WriteLine("=================================");
            List<Tasks> tasks = _taskRepository.Get();

            foreach (var item in tasks.Select((x, i) => new { Title = x.Title, Description = x.Description, DateDue = x.DateDue, DateCreation = x.DateCreation, DateCompletion = x.DateCompletion, Status = x.Status, NameUser = x.NameUser, NameCategory = x.NameCategory, index = i }))
            {
                if (item.Status == StatusTask.Concluida)
                {
                    Console.WriteLine($" Id: [ {item.index} ] = TITULO : {item.Title} | DESCRICAO : {item.Description} | DATA VENCIMENTO : {item.DateDue} | DATA CRIAÇÃO {item.DateCreation} | DATA COnCLUSAO {item.DateCompletion} |STATUS : {item.Status} | CATEGORIA : {item.NameCategory} | RESPONSAVEL : {item.NameUser}");
                    Console.WriteLine("========================================");
                }
                else
                {
                    Console.WriteLine($" Id: [ {item.index} ] = TITULO : {item.Title} | DESCRICAO : {item.Description} | DATA VENCIMENTO : {item.DateDue} | DATA CRIAÇÃO {item.DateCreation} | STATUS : {item.Status} | CATEGORIA : {item.NameCategory} | RESPONSAVEL : {item.NameUser}");
                    Console.WriteLine("========================================");
                }

            }

            int idChosen;
            Console.WriteLine("ESCOLHA O ID DA TAREFA QUE DESEJA EDITAR");
            idChosen = int.Parse(Console.ReadLine());

            if (idChosen < 0 || idChosen >= tasks.Count)
            {
                Console.WriteLine("ID INVALIDO");
                return;
            }

            Tasks tasksEditing = tasks[idChosen];
            Console.Clear();

            while (status == "" || status == null)
            {
                Console.WriteLine($"ESCOLHA O STATUS [Atual é {tasksEditing.Status}]");
                Console.WriteLine("[ 1 ] - Pendente");
                Console.WriteLine("[ 2 ] - Em Andamento");
                Console.WriteLine("[ 3 ] Cancelada");

                string chooseStatus = Console.ReadLine();

                if (chooseStatus == "1")
                {
                    status = "Pendente";
                    statusTask = StatusTask.Pendente;
                }
                else if (chooseStatus == "2")
                {
                    status = "Em Andamento";
                    statusTask = StatusTask.EmAndamento;
                }
                else if (chooseStatus == "3")
                {
                    status = "Cancelada";
                    statusTask = StatusTask.Cancelada;
                }
                else
                {
                    Console.WriteLine("Escolha Invalida escolha outra");
                    
                }
            }


            while (title == "" || title == null)
            {
                Console.WriteLine($"Qual novo Titulo que deseja colocar [Atual : {tasksEditing.Title}]");
                title = Console.ReadLine();

                if (title == "")
                {
                    Console.WriteLine("TitulO NÃO pode ser vazio");
                }

            }

            while (description == "" || description == null)
            {
                Console.WriteLine($"Qual nova Descrição que deseja colocar [Atual : {tasksEditing.Description}]");
                description = Console.ReadLine();

                if (description == "")
                {
                    Console.WriteLine("Descricao NÃO pode ser vazio");
                }

            }

            while (dueDateString == "" || dueDateString == null)
            {
                try
                {
                    Console.WriteLine($"Qual a nova Data de Vencimento que deseja [Atual: {tasksEditing.DateDue}]");
                    dueDateString = Console.ReadLine();
                    dueDate = DateTime.Parse(dueDateString);
                    Console.WriteLine(dueDate);
                    if (dueDate < DateTime.Now)
                    {
                        Console.WriteLine("Data de Vencimento não pode ser no passado");
                        dueDateString = "";
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Data de Vencimento Invalida Insira outra (FORMATO ERRADO OU DATA JA PASSOU)");
                    dueDateString = "";
                }
            }

            while (nameCategory == "" || nameCategory == null)
            {
                Console.WriteLine("LISTAGEM DE CATEGORIAS");
                Console.WriteLine("======================================");
                List<Category> categorys = _categoryRepository.Get();


                foreach (var item in categorys.Select((x, i) => new { Value = x.Name, index = i }))
                {
                    Console.WriteLine($" [ {item.index} ] => {item.Value}");
                }

                if (categorys.Count == 0)
                {
                    Console.WriteLine("Não existe Categorias");
                    break;
                }

                Console.WriteLine($"Escolha o id Da Categoria que quer selecionar [A atual é {tasksEditing.NameCategory}]");
                int idCategory = int.Parse(Console.ReadLine());

                if (idCategory < 0 || idCategory >= categorys.Count)
                {
                    Console.WriteLine("Não possui essa categoria");
                    nameCategory = "";
                }
                else
                {
                    nameCategory = categorys[idCategory].Name;
                }

            }

            while (nameUser == "" || nameUser == null)
            {
                Console.WriteLine("LISTAGEM DE USUARIOS");
                Console.WriteLine("======================================");
                List<User> users = _userRepository.Get();

                if (users.Count == 0)
                {
                    break;
                }

                foreach (var item in users.Select((x, i) => new { Value = x.Name, index = i }))
                {
                    Console.WriteLine($" [ {item.index} ] => {item.Value}");
                }

                Console.WriteLine($"Escolha o ID do Usuario que quer selecionar [A atual é {tasksEditing.NameUser}]");
                int idUser = int.Parse(Console.ReadLine());

                if (idUser < 0 || idUser >= users.Count)
                {
                    Console.WriteLine("Não possui essa categoria");
                    nameUser = "";
                }
                else
                {
                    nameUser = users[idUser].Name;
                }

            }

            tasksEditing.UpdateTask(title, description, DateTime.Parse(dueDateString),nameCategory,nameUser,statusTask);
            _taskRepository.Update(tasks);
        }

        public void GetTaskToMarkAsComplete()
        {
            Console.WriteLine("LISTAGEM DE TAREFAS");
            Console.WriteLine("=================================");
            List<Tasks> tasks = _taskRepository.Get();

            foreach (var item in tasks.Select((x, i) => new { Title = x.Title, Description = x.Description, DateDue = x.DateDue, DateCreation = x.DateCreation, DateCompletion = x.DateCompletion, Status = x.Status, NameUser = x.NameUser, NameCategory = x.NameCategory, index = i }))
            {
                if (item.Status == StatusTask.Concluida)
                {
                    Console.WriteLine($" Id: [ {item.index} ] = TITULO : {item.Title} | DESCRICAO : {item.Description} | DATA VENCIMENTO : {item.DateDue} | DATA CRIAÇÃO {item.DateCreation} | DATA COnCLUSAO {item.DateCompletion} |STATUS : {item.Status} | CATEGORIA : {item.NameCategory} | RESPONSAVEL : {item.NameUser}");
                    Console.WriteLine("========================================");
                }
                else
                {
                    Console.WriteLine($" Id: [ {item.index} ] = TITULO : {item.Title} | DESCRICAO : {item.Description} | DATA VENCIMENTO : {item.DateDue} | DATA CRIAÇÃO {item.DateCreation} | STATUS : {item.Status} | CATEGORIA : {item.NameCategory} | RESPONSAVEL : {item.NameUser}");
                    Console.WriteLine("========================================");
                }

            }

            int idChosen;
            Console.WriteLine(" Escolha o id da Tarefa que deseja Marcar como concluída");
            while (!int.TryParse(Console.ReadLine(),out idChosen)){
                Console.WriteLine(" Id tem que ser um numero inteiro ");
            }

            Tasks taskEditing = tasks[idChosen];
            taskEditing.UpdateStatus(StatusTask.Concluida);
            _taskRepository.Update(tasks);
        }

        public void ListTasksSortedDueDate()
        {
            //MOSTRAR ORDENADAS POR DATA DE VENCIMENTO
            Console.WriteLine("LISTAGEM DE TAREFAS POR DATA DE VENCIMENTO");
            Console.WriteLine("=================================");
            List<Tasks> tasks = _taskRepository.Get();

            var newList = tasks
                .OrderBy(i => i.DateDue)
                .ToList();

            foreach (var item in newList.Select((x, i) => new { Title = x.Title, Description = x.Description, DateDue = x.DateDue, DateCreation = x.DateCreation, DateCompletion = x.DateCompletion, Status = x.Status, NameUser = x.NameUser, NameCategory = x.NameCategory, index = i }))
            {
                if (item.Status == StatusTask.Concluida)
                {
                    Console.WriteLine($" Id: [ {item.index} ] = TITULO : {item.Title} | DESCRICAO : {item.Description} | DATA VENCIMENTO : {item.DateDue} | DATA CRIAÇÃO {item.DateCreation} | DATA CONCLUSAO {item.DateCompletion} |STATUS : {item.Status} | CATEGORIA : {item.NameCategory} | RESPONSAVEL : {item.NameUser}");
                    Console.WriteLine("========================================");
                }
                else
                {
                    Console.WriteLine($" Id: [ {item.index} ] = TITULO : {item.Title} | DESCRICAO : {item.Description} | DATA VENCIMENTO : {item.DateDue} | DATA CRIAÇÃO {item.DateCreation} | STATUS : {item.Status} | CATEGORIA : {item.NameCategory} | RESPONSAVEL : {item.NameUser}");
                    Console.WriteLine("========================================");
                }
            }
        }

        public void ListOverdueTasks()
        {
            //ORDENAR TAREFAS ATRASADAS
            Console.WriteLine("LISTAGEM DE TAREFAS POR DATA DE VENCIMENTO");
            Console.WriteLine("=================================");
            List<Tasks> tasks = _taskRepository.Get();

            var newList = tasks
                .Where(i => i.DateDue <= DateTime.Now)
                .Where(i => i.Status != StatusTask.Concluida)
                .ToList();

            foreach (var item in newList.Select((x, i) => new { Title = x.Title, Description = x.Description, DateDue = x.DateDue, DateCreation = x.DateCreation, DateCompletion = x.DateCompletion, Status = x.Status, NameUser = x.NameUser, NameCategory = x.NameCategory, index = i }))
            {
                if (item.Status == StatusTask.Concluida)
                {
                    Console.WriteLine($" Id: [ {item.index} ] = TITULO : {item.Title} | DESCRICAO : {item.Description} | DATA VENCIMENTO : {item.DateDue} | DATA CRIAÇÃO {item.DateCreation} | DATA CONCLUSAO {item.DateCompletion} |STATUS : {item.Status} | CATEGORIA : {item.NameCategory} | RESPONSAVEL : {item.NameUser}");
                    Console.WriteLine("========================================");
                }
                else
                {
                    Console.WriteLine($" Id: [ {item.index} ] = TITULO : {item.Title} | DESCRICAO : {item.Description} | DATA VENCIMENTO : {item.DateDue} | DATA CRIAÇÃO {item.DateCreation} | STATUS : {item.Status} | CATEGORIA : {item.NameCategory} | RESPONSAVEL : {item.NameUser}");
                    Console.WriteLine("========================================");
                }
            }
        }

        public void FilterTasksByStatus()
        {
            List<Tasks> tasks = _taskRepository.Get();

            Console.WriteLine($"ESCOLHA O STATUS QUE DESEJA FILTRAR");
            Console.WriteLine("[ 1 ] - Pendente");
            Console.WriteLine("[ 2 ] - Em Andamento");
            Console.WriteLine("[ 3 ] Cancelada");
            Console.WriteLine("[ 4 ] Concluida");

            int statusChosen;
            Console.WriteLine(" Escolha o id da Tarefa que deseja Marcar como concluída");
            while(!int.TryParse(Console.ReadLine(),out statusChosen))
            {
                Console.WriteLine("Apenas Permitido Numeros Inteiros");
            }

            if(statusChosen > 4 || statusChosen <= 0)
            {
                Console.WriteLine("Opção Inválida");
                return;
            }

            StatusTask statusTask = StatusTask.Pendente;
            if (statusChosen == 1)
            {
                statusTask = StatusTask.Pendente;
            }else if (statusChosen == 2)
            {
                statusTask = StatusTask.EmAndamento;
            }else if (statusChosen == 3)
            {
                statusTask = StatusTask.Cancelada;
            }else if (statusChosen == 4)
            {
                statusTask = StatusTask.Concluida;
            }

            var listaTarefasFiltrada = tasks
                                       .Where(i => i.Status == statusTask)
                                       .ToList();

            foreach (var item in listaTarefasFiltrada.Select((x, i) => new { Title = x.Title, Description = x.Description, DateDue = x.DateDue, DateCreation = x.DateCreation, DateCompletion = x.DateCompletion, Status = x.Status, NameUser = x.NameUser, NameCategory = x.NameCategory, index = i }))
            {
                if(statusChosen == 4)
                {
                    Console.WriteLine($" Id: [ {item.index} ] = TITULO : {item.Title} | DESCRICAO : {item.Description} | DATA VENCIMENTO : {item.DateDue} | DATA CRIAÇÃO {item.DateCreation} | DATA CONCLUSAO {item.DateCompletion} |STATUS : {item.Status} | CATEGORIA : {item.NameCategory} | RESPONSAVEL : {item.NameUser}");
                    Console.WriteLine("========================================");
                }
                else
                {
                    Console.WriteLine($" Id: [ {item.index} ] = TITULO : {item.Title} | DESCRICAO : {item.Description} | DATA VENCIMENTO : {item.DateDue} | DATA CRIAÇÃO {item.DateCreation} | STATUS : {item.Status} | CATEGORIA : {item.NameCategory} | RESPONSAVEL : {item.NameUser}");
                    Console.WriteLine("========================================");
                }
                
            }

        }

        public void FilterTasksByCategory()
        {
            string nameCategory = "";
            while (nameCategory == "" || nameCategory == null)
            {
                Console.WriteLine("LISTAGEM DE CATEGORIAS");
                Console.WriteLine("======================================");
                List<Category> categorys = _categoryRepository.Get();


                foreach (var item in categorys.Select((x, i) => new { Value = x.Name, index = i }))
                {
                    Console.WriteLine($" [ {item.index} ] => {item.Value}");
                }

                if (categorys.Count == 0)
                {
                    Console.WriteLine("Não existe Categorias");
                    break;
                }

                Console.WriteLine($"Escolha o id Da Categoria que quer selecionar para Filtrar");
                int idCategory;
                while (!int.TryParse(Console.ReadLine(),out idCategory))
                {
                    Console.WriteLine($"Invalido pode apenas escolher Números");
                }

                if (idCategory < 0 || idCategory >= categorys.Count)
                {
                    Console.WriteLine("Não possui essa categoria");
                }
                else
                {
                    nameCategory = categorys[idCategory].Name;
                }

            }

            List<Tasks> tasks = _taskRepository.Get();

            List<Tasks> filteredTasks;

            filteredTasks = tasks
                               .Where(i => i.NameCategory == nameCategory)
                               .ToList();

            if (filteredTasks.Count == 0)
            {
                Console.WriteLine("Não possui nenhuma tarefa pra esse status");
                return;
            }

            foreach (var item in filteredTasks.Select((x, i) => new { Title = x.Title, Description = x.Description, DateDue = x.DateDue, DateCreation = x.DateCreation, DateCompletion = x.DateCompletion, Status = x.Status, NameUser = x.NameUser, NameCategory = x.NameCategory, index = i }))
            {
                Console.WriteLine($" Id: [ {item.index} ] = TITULO : {item.Title} | DESCRICAO : {item.Description} | DATA VENCIMENTO : {item.DateDue} | DATA CRIAÇÃO {item.DateCreation} | STATUS : {item.Status} | CATEGORIA : {item.NameCategory} | RESPONSAVEL : {item.NameUser}");
                Console.WriteLine("========================================");
            }

        }

    }
}
