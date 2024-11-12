using gestiondette.entities;
using gestiondette.Services.Impl;
using gestiondette.Services;
using gestiondette.Views;
namespace gestiondette.views
{
    public class ClientView : ViewImpl<Client>
    {
        private readonly UserView userView;
        private readonly IClientService clientService;
        private readonly IUserService userService;

        public ClientView(UserView userView, IClientService clientService, IUserService userService)
        {
            this.userView = userView;
            this.clientService = clientService;
            this.userService = userService;
        }

        public Client Saisie()
        {
            var client = new Client { Solde = 0.0 };
            Console.Write("Saisir le nom du client: ");
            client.Surnom = Console.ReadLine();
            Console.Write("Saisir le téléphone du client: ");
            client.Telephone = Console.ReadLine();
            Console.Write("Saisir l'adresse du client: ");
            client.Adresse = Console.ReadLine();

            var user = Ask();
            if (user != null)
            {
                client.User = user;
                user.Client = client;

                // userService.Save(user);
            }

            return client;
        }

        private User Ask()
        {
            Console.WriteLine("Voulez-vous créer un compte pour ce client ? 1-Oui 2-Non");
            if (int.TryParse(Console.ReadLine(), out int resp) && resp == 1)
            {
                return userView.SaisieClient();
                // Enregistrez l'utilisateur si nécessaire
                // userService.Save(user);
            }

            return null;
        }

        public void Finding()
        {
            Console.Write("Entrez le numéro de téléphone du client: ");
            string tel = Console.ReadLine();
            var client = clientService.findByTelephone(tel);
            if (client == null)
            {
                Console.WriteLine("Client non existant");
            }
            else
            {
                Console.WriteLine(client);
            }
        }

        public Client UpdateClient()
        {
            Console.Write("Entrez le numéro de téléphone du client: ");
            string tel = Console.ReadLine();
            var client = clientService.findByTelephone(tel);

            if (client == null)
            {
                Console.WriteLine("Client non existant");
                return null;
            }
            else if (client.User != null)
            {
                Console.WriteLine("Le client possède déjà un compte");
                return null;
            }

            var user = userView.SaisieClient();
            client.User = user;
            user.Client = client;
            userService.Save(user);
            clientService.Update(client);

            Console.WriteLine("Client mis à jour !");
            return client;
        }

        public List<Client> Show()
        {
            var clientsWithAccounts = new List<Client>();
            var clients = clientService.FindAll();

            foreach (var client in clients)
            {
                if (client.User != null)
                {
                    clientsWithAccounts.Add(client);
                }
            }

            return clientsWithAccounts;
        }

        public static void ListClients(List<Client> clients)
        {
            foreach (var client in clients)
            {
                Console.WriteLine(client);
            }
        }

        public static Client CreateClient()
        {
            Console.Write("Nom : ");
            string nom = Console.ReadLine();
            Console.Write("Téléphone : ");
            string telephone = Console.ReadLine();
            Console.Write("Adresse : ");
            string adresse = Console.ReadLine();
            return new Client { Surnom = nom, Telephone = telephone, Adresse = adresse };
        }

        public static void UpdateClientInfo(Client client)
        {
            Console.Write("Nouveau nom : ");
            string newNom = Console.ReadLine();
            Console.Write("Nouveau téléphone : ");
            string newTelephone = Console.ReadLine();
            Console.Write("Nouvelle adresse : ");
            string newAdresse = Console.ReadLine();
            client.Surnom = newNom;
            client.Telephone = newTelephone;
            client.Adresse = newAdresse;
            Console.WriteLine("Client modifié!");
        }

        public static int DeleteClient()
        {
            Console.Write("Voulez-vous supprimer un client ? (o/n) ");
            string answer = Console.ReadLine();
            if (answer.ToLower() == "o")
            {
                Console.Write("Id du client à supprimer : ");
                return Convert.ToInt32(Console.ReadLine());
            }
            return 0;
        }

        public static int SaisirId()
        {
            Console.Write("Id du client ? ");
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
