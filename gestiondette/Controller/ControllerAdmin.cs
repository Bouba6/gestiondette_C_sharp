using gestiondette.entities;
using gestiondette.Services;
using gestiondette.views;

namespace gestiondette.Controller
{
    public class ControllerAdmin
    {
        private readonly IArticleService articleService;
        private readonly IClientService clientService;
        private readonly ClientView clientView;
        private readonly IUserService userService;
        private readonly UserView userView;
        private readonly ArticleView articleView;

        public ControllerAdmin(
            IArticleService articleService,
            ClientView clientView,
            IClientService clientService,
            IUserService userService,
            UserView userView,
            ArticleView articleView
        )
        {
            this.articleService = articleService;
            this.clientView = clientView;
            this.clientService = clientService;
            this.userService = userService;
            this.userView = userView;
            this.articleView = articleView;
        }

        public void Load()
        {
            Console.WriteLine("Bonjour Admin !");
            int choixAdmin;
            do
            {
                choixAdmin = menuAdmin();

                switch (choixAdmin)
                {
                    case 1:
                        clientView.afficher(clientService.FindAll());
                        break;
                    case 2:
                        User user = userView.Saisie();
                        if (user != null)
                        {
                            userService.Save(user);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("user non enregistre");
                        }
                        break;

                    case 3:
                        userView.afficher(userService.FindAll());
                        userView.ChangeState();
                        break;
                    case 4:
                        userView.afficher(userService.FindAll());
                        break;
                    case 5:
                        userView.afficher(userService.FindAll());
                        userView.afficher(userView.FindByRole());
                        break;
                    case 6:
                        userView.afficher(userService.FindAll());
                        userView.afficher(userView.FindByEtat());
                        break;
                    case 7:
                        Article article = articleView.saisie();
                        if (article != null)
                        {
                            articleService.Save(article);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("article non enregistre");
                        }
                        break;
                    case 8:
                        articleView.afficher(articleService.FindAll());
                        break;
                    case 9:
                        articleView.filter();
                        break;
                    case 10:
                        articleView.afficher(articleService.FindAll());
                        Article article1 = articleView.changeQte();
                        if (article1 != null)
                        {
                            articleService.Update(article1);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("article non modifie");
                        }
                        break;

                    case 0:
                        Console.WriteLine("Au revoir!");
                        break;
                    default:
                        Console.WriteLine("Choix invalide!");
                        break;
                }
            } while (choixAdmin != 0);
        }

        private int menuAdmin()
        {
            Console.WriteLine("1---Creer un compte pour un client sans compte");
            Console.WriteLine("2---Creer un compte user");
            Console.WriteLine("3---Desactiver un compte User");
            Console.WriteLine("4---Lister les users");
            Console.WriteLine("5---Afficher les comptes par role");
            Console.WriteLine("6---Afficher les comptes actifs");
            Console.WriteLine("7---Saisie Article");
            Console.WriteLine("8--Afficher Article");
            Console.WriteLine("9--Filtrer Article par disponibilité");
            Console.WriteLine("10--Mettre à jour la quantité en stock");
            Console.WriteLine("0--Quitter");

            Console.Write("Votre choix: ");
            return int.TryParse(Console.ReadLine(), out int choix) ? choix : -1;
        }
    }
}
