using gestiondette.entities;
using gestiondette.Services;
using gestiondette.Services.Impl;
using gestiondette.Views;
using gestiondette.Enum;

namespace gestiondette.views
{
    public class UserView : ViewImpl<User>
    {
        private readonly IUserService userService;

        public UserView(IUserService userService)
        {
            this.userService = userService;
        }

        public User Saisie()
        {
            User user = new User();

            Console.WriteLine("Saisir l'email ");
            user.Login = Console.ReadLine();

            user.Email = user.Login;

            Console.WriteLine("Saisir le login ");
            user.Login = Console.ReadLine();

            while (userService.findByLogin(user.Login, user.Password) != null)
            {
                Console.WriteLine("Login existant. Saisir le login ");
                user.Login = Console.ReadLine();
            }

            Console.WriteLine("Saisir le mot de passe ");
            user.Password = Console.ReadLine();
            user.State = true;
            user.Role = AskRole();



            return user;
        }

        public User SaisieClient()
        {
            User user = new User();

            Console.WriteLine("Saisir l'email ");
            user.Login = Console.ReadLine();

            Console.WriteLine("Saisir le login ");
            user.Login = Console.ReadLine();
            user.Email = "nanoa";
            Console.WriteLine("Saisir le mot de passe ");
            user.Password = Console.ReadLine();
            user.State = true;
            user.Role = Role.CLIENT;

            return user;
        }

        public List<User> FindByRole()
        {
            Role role = AskRole();
            return userService.findByRole(role);
        }

        public List<User> FindByEtat()
        {
            bool etat = AskEtat();
            return userService.findByState(etat);
        }

        private Role AskRole()
        {
            Console.WriteLine("Entrer le rôle que vous recherchez: 1-Admin 2-Boutiquier 3-Client");
            int choix;
            while (!int.TryParse(Console.ReadLine(), out choix) || choix < 1 || choix > 3)
            {
                Console.WriteLine("Choix invalide, veuillez entrer 1, 2 ou 3.");
            }

            return choix switch
            {
                1 => Role.ADMIN,
                2 => Role.BOUTIQUIER,
                _ => Role.CLIENT
            };
        }

        private bool AskEtat()
        {
            Console.WriteLine("Entrer l'état que vous recherchez: 1-Activer 2-Désactiver");
            int choix;
            while (!int.TryParse(Console.ReadLine(), out choix) || choix < 1 || choix > 2)
            {
                Console.WriteLine("Choix invalide, veuillez entrer 1 ou 2.");
            }

            return choix == 1 ? true : false;
        }

        public User ChangeState()
        {
            Console.WriteLine("Entrer l'ID du user");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID invalide.");
                return null;
            }

            User user = userService.FindById(id);
            if (user == null)
            {
                Console.WriteLine("Utilisateur non existant.");
                return null;
            }

            Ask(user);
            return user;
        }

        private void Ask(User user)
        {
            Console.WriteLine($"Vous venez de changer l'état de cet utilisateur, qui était {user.State}");
            user.State = user.State == true ? false : true;
        }
    }
}
