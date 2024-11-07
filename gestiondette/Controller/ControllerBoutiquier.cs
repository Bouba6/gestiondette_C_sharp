using gestiondette.Views;
using gestiondette.Services;
using gestiondette.views;
using gestiondette.entities;
using gestiondette.Enum;

namespace gestiondette.Controller
{
    public class ControllerBoutiquier
    {
        private readonly IClientService clientService;

        private readonly IDetteService detteService;
        private readonly DetteView detteView;
        private readonly PaiementView paiementView;

        private readonly ClientView clientView;

        private readonly IPaiementService paiementService;

        public ControllerBoutiquier(
            IClientService clientService,

            IDetteService detteService,
            DetteView detteView,
            PaiementView paiementView,
            ClientView clientView,
            IPaiementService paiementService
            )
        {
            this.clientService = clientService;

            this.detteService = detteService;
            this.detteView = detteView;
            this.paiementView = paiementView;
            this.clientView = clientView;
            this.paiementService = paiementService;

        }

        public void Load()
        {
            Console.WriteLine("Bonjour Boutiquier !");
            int choixBoutiquier;
            do
            {
                choixBoutiquier = menuBoutiquier();

                switch (choixBoutiquier)
                {
                    case 1:
                        Client client = clientView.Saisie();  // Création d'un nouveau client
                        if (client != null)
                        {
                            clientService.Save(client);  // Sauvegarde du client
                            break;
                        }
                        else
                        {
                            Console.WriteLine("client non enregistre");
                        }
                        break;
                    case 2:
                        clientView.afficher(clientService.FindAll());  // Affichage des clients
                        break;

                    case 3:
                        clientView.afficher(clientService.FindAll());
                        clientView.Finding();
                        break;
                    case 4:
                        Dette dette = detteView.Saisie();
                        dette.EtatDette = EtatDette.VALIDER;
                        Console.WriteLine(dette);
                        detteService.Save(dette);
                        break;

                    case 5:
                        detteView.afficher(detteService.FindAll());
                        break;
                    case 6:
                        Paiement paiement = paiementView.saisie();
                        if (paiement != null)
                        {
                            paiementService.Save(paiement);

                            break;
                        }
                        else
                        {
                            Console.WriteLine("Paiement non enregistrer");
                        }
                        break;



                    case 7:
                        Console.WriteLine("Entrer le numero de telephone du client");
                        String tel = Console.ReadLine();
                        detteView.FindDette(tel);
                        break;
                    case 8:
                        detteView.showDetteWithFilter();
                        break;
                    case 0:
                        Console.WriteLine("Au revoir!");
                        break;



                    default:
                        Console.WriteLine("Choix invalide!");
                        break;
                }
            } while (choixBoutiquier != 0);

        }


        public static int menuBoutiquier()
        {
            Console.WriteLine("1---Créer un client");
            Console.WriteLine("2---Lister Clients");
            Console.WriteLine("3--Lister les informations d'un client par son numéro");
            Console.WriteLine("4--Saisie dette");
            Console.WriteLine("5--Lister dette");
            Console.WriteLine("6--Faire le paiement d'une dette");
            Console.WriteLine("7--Lister dette non payée");
            Console.WriteLine("8--Lister les demandes de dettes en cours");
            Console.WriteLine("0--QUITTER");
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
