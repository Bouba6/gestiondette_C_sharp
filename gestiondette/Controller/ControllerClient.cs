using gestiondette.Services;
using gestiondette.views;
using gestiondette.entities;
using gestiondette.Enum;
using gestiondette.Core;

namespace gestiondette.Controller
{
    public class ControllerClient
    {
        private readonly IClientService clientService;
        private readonly DetteView detteView;

        private readonly IDetteService detteService;
        public ControllerClient(IClientService clientService, DetteView detteView, IDetteService detteService)
        {
            this.clientService = clientService;
            this.detteView = detteView;
            this.detteService = detteService;

        }

        public void Load()
        {
            Console.WriteLine("Bonjour Client !");
            int choixClient;
            do
            {
                choixClient = menuClient();
                User user = UserConnected.getUserConnected();
                Console.WriteLine(user);

                Client client = clientService.getClientByUser(user);
                switch (choixClient)
                {
                    case 1:
                        detteView.FindDette(client.Telephone);
                        break;
                    case 2:
                        Dette dette = detteView.Saisie();
                        if (dette != null)
                        {
                            dette.EtatDette = EtatDette.ENCOURS;
                            detteService.Save(dette);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("dette non enregistre");
                        }
                        break;

                    case 3:
                        Console.WriteLine(client);
                        detteView.listDemandeDette(client);
                        break;
                    case 4:
                        detteView.Relance(client);
                        break;
                }
            } while (choixClient != 0);
        }

        public static int menuClient()
        {
            Console.WriteLine("1--Lister dette non payée");
            Console.WriteLine("2--Faire une demande de dette");
            Console.WriteLine("3--Lister ces demandes de dette");
            Console.WriteLine("4--Envoyer une relance pour une  demande de dette annulere");
            Console.WriteLine("0--QUITTER");
            return Convert.ToInt32(Console.ReadLine());
        }



    }
}