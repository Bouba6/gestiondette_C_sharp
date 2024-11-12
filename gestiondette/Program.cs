using gestiondette.entities;
using gestiondette.Services.Impl;
using gestiondette.Services;
using gestiondette.views;
using gestiondette.Repository;
using gestiondette.Repository.List;
using gestiondette.Core;
using gestiondette.Enum;

using gestiondette.Controller;
using gestiondette.Auth;
using gestiondette.Repository.Bd;
using gestiondette.core;
using gestiondette.core.Factory;

namespace Cours
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Persistence persistence = Persistence.Bd1;
            // Initialisation des services
            IdetailRepository detailRepository = (IdetailRepository)FactoryRepo<DetailDette>.CreateRepository(persistence);
            IArticleRepository articleRepository = (IArticleRepository)FactoryRepo<Article>.CreateRepository(persistence);
            IdetteRepositorie detteRepositorie = (IdetteRepositorie)FactoryRepo<Dette>.CreateRepository(persistence);
            IUserRepository userRepository = (IUserRepository)FactoryRepo<User>.CreateRepository(persistence);
            IPaiementRepository paiementRepository = (IPaiementRepository)FactoryRepo<Paiement>.CreateRepository(persistence);
            IClientRepository clientRepository = (IClientRepository)FactoryRepo<Client>.CreateRepository(persistence);



            IdetailService detailService = new DetailServiceImpl(detailRepository);
            IArticleService articleService = new ArticleServiceImpl(articleRepository);
            IDetteService detteService = new DetteServiceImpl(detteRepositorie);
            IUserService userService = new UserServiceImpl(userRepository);
            IClientService clientService = new ClientServiceImpl(clientRepository);
            IPaiementService paiementService = new PaiementServiceImpl(paiementRepository);



            UserView userView = new UserView(userService);
            DetteView detteView = new DetteView(articleService, clientService, detteService);
            ClientView clientView = new ClientView(userView, clientService, userService);
            PaiementView paiementView = new PaiementView(clientService, detteService);
            ArticleView articleView = new ArticleView(articleService);

            // int choice;
            // User user1 = new User();
            // User user2 = new User();
            // User user3 = new User();
            // Client client1 = new Client();
            // user2.Login = "admin";
            // user2.Password = "admin";
            // user2.Role = Role.ADMIN;

            // userService.Save(user2);

            // user1.Login = "boutiquier";
            // user1.Password = "boutiquier";
            // user1.Role = Role.BOUTIQUIER;
            // userService.Save(user1);

            // user3.Login = "client";
            // user3.Password = "client";
            // client1.Surnom = "client";
            // client1.Telephone = "776404098";
            // client1.Adresse = "addresse";
            // user3.Client = client1;
            // client1.User = user3;
            // user3.Role = Role.CLIENT;
            // clientService.Save(client1);
            // userService.Save(user3);

            // clientView.afficher(clientService.FindAll());
            // Console.WriteLine("c'est la liste des utilisateurs");
            // userView.afficher(userService.FindAll());
            // Console.WriteLine("c'est la liste des utilisateurs");

            int output = 0;
            do
            {
                // userService.Save(userView.Saisie());
                // clientService.Save(clientView.Saisie());
                // clientView.afficher(clientService.FindAll());
                Connexion connection = new Connexion(userService);
                User user = connection.connexion();
                // userView.afficher(userService.FindAll());


                if (user != null)
                {
                    UserConnected.setUserConnected(user);

                    switch (user.Role)
                    {
                        case Role.BOUTIQUIER:
                            ControllerBoutiquier controllerBoutiquier = new ControllerBoutiquier(clientService, detteService, detteView, paiementView, clientView, paiementService);
                            controllerBoutiquier.Load();
                            break;

                        case Role.ADMIN:
                            ControllerAdmin controllerAdmin = new ControllerAdmin(articleService, clientView, clientService, userService, userView, articleView);
                            controllerAdmin.Load();
                            break;

                        case Role.CLIENT:
                            ControllerClient controllerClient = new ControllerClient(clientService, detteView, detteService);
                            controllerClient.Load();
                            break;

                        default:
                            Console.WriteLine("Choix invalide!");
                            break;
                    }
                }
            } while (output != 20);
        }








    }
}
