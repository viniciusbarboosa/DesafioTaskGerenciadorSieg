using System;
using System.Collections.Generic;
using System.Text;
using TaskManagerConsole.Entities;
using TaskManagerConsole.Helpers;
using TaskManagerConsole.Repositories;
using TaskManagerConsole.Repositories.interfaces;

namespace TaskManagerConsole.Services
{
    public class UserService
    {
        IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository; 
        }
        public void CreateUser()
        {
            Console.WriteLine("Digite o nome do novo Usuário");
            string user = Console.ReadLine();
            Console.WriteLine("Digite o nome do email do novo Usuário");
            string email = Console.ReadLine();
            
           
            
            bool emailValid = ValidationHelper.IsValidEmail(email);
            if (!emailValid) {
                Console.WriteLine("Email inválido");
                return;
            }

        List<User> users = _userRepository.GetUsers();

            foreach (var item in users.Select((x, i) => new { Value = x.Name, index = i }))
            {
                if (item.Value == user)
                {
                    Console.WriteLine("Não Possivel Criar Usuario Com Nome que ja existe");
                    return;
                }
            }

            User newUser = new User(user,email);

            _userRepository.CreateUser(newUser);

            Console.WriteLine("Usuário CRIADO COM SUCESSO");
        }

        public void GetUsers()
        {
            Console.WriteLine("LISTAGEM DE USUÁRIOS");
            Console.WriteLine("======================================");
            var usuarios = _userRepository.GetUsers();
            foreach (var item in usuarios.Select((x, i) => new { Name = x.Name, Email = x.Email, index = i }))
            {
                Console.WriteLine($" {item.Name} - Email : {item.Email}");
            }

        }
    }
}
