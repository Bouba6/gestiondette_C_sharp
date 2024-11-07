using gestiondette.Services;
using gestiondette.entities;

namespace gestiondette.Auth
{
    public class Connexion
    {
        private readonly IUserService userService;

        // Constructor to inject userService dependency
        public Connexion(IUserService userService)
        {
            this.userService = userService;
        }

        public User connexion()
        {
            Console.WriteLine("Enter your login: ?");
            string login = Console.ReadLine();

            Console.WriteLine("Enter your password: ?");
            string password = Console.ReadLine();


            User user = userService.findByLogin(login, password);

            if (user == null)
            {
                Console.WriteLine("Invalid login or password.");
                return null;
            }

            Console.WriteLine($"Welcome {user.Login}!");
            return user;
        }
    }
}
