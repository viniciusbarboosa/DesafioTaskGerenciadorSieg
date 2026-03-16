using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagerConsole.Views
{
    public class Menu
    {
        public static void ShowMenu()
        {
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
            Console.WriteLine("[13] Listar Tarefas Filtradas por Status");
            Console.WriteLine("[14] Listar Tarefas Filtradas por Categoria");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("[15]  Sair");
            Console.ResetColor();
        }
    }
}
