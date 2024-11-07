using gestiondette.Core;
using gestiondette.core;
using gestiondette.Services;
using gestiondette.Services.Impl;
using gestiondette.Enum;
using gestiondette.Views;
using gestiondette.views;
using gestiondette.entities;

namespace gestiondette.views
{
    public class DetteView : ViewImpl<Dette>
    {
        private IArticleService articleService;
        private IClientService clientService;
        private IDetteService detteService;

        public DetteView(IArticleService articleService, IClientService clientService, IDetteService detteService)
        {
            this.articleService = articleService;
            this.clientService = clientService;
            this.detteService = detteService;
        }

        public Dette Saisie()
        {
            int attempts = 0;
            Client client = null;
            string tel = null;

            // Allow up to 3 attempts to find the client by phone number
            while (attempts < 3 && client == null)
            {
                Console.WriteLine("Enter the client's phone number:");
                User user = UserConnected.getUserConnected();

                // If the user is a CLIENT, use their phone number, otherwise, prompt for one
                tel = user.Role.CompareTo(Role.CLIENT) == 0 ? user.Client.Telephone : Console.ReadLine();

                // Try to find the client by phone number
                client = clientService.findByTelephone(tel);

                if (client == null)
                {
                    attempts++;
                    string message = attempts < 3
                        ? $"Client does not exist. You have {3 - attempts} attempt(s) left. Please try again."
                        : "Client does not exist after 3 attempts.";

                    Console.WriteLine(message);

                    // Return null if not found after 3 attempts
                    if (attempts >= 3)
                    {
                        return null;
                    }
                }
            }

            // Proceed with creating a Dette if the client was found
            return AskDette(client);
        }



        public void Relance(Client client)
        {
            List<Dette> dettes = detteService.ListDetEc(client);
            if (dettes.Count == 0)
            {
                Console.WriteLine("No debts found");
                return;
            }
            Console.WriteLine("Enter the Dette ID:");
            int id = int.Parse(Console.ReadLine());
            Dette dette = detteService.FindById(id);
            if (dette == null)
            {
                Console.WriteLine("Dette does not exist");
                return;
            }
            if (dette.EtatDette == EtatDette.ANNULER)
            {
                dette.EtatDette = EtatDette.ENCOURS;
                detteService.Update(dette);
            }
        }

        public Dette AskDette(Client client)
        {
            Dette dette = new Dette();
            while (true)
            {
                Console.WriteLine("Enter the Article ID:");
                int id = int.Parse(Console.ReadLine());
                Article article = articleService.FindById(id);
                if (article == null)
                {
                    Console.WriteLine("Article does not exist");
                    continue;
                }
                int qte;
                int attempts = 0;
                const int maxAttempts = 3;
                do
                {
                    Console.WriteLine(article);
                    Console.WriteLine("Enter quantity:");
                    qte = int.Parse(Console.ReadLine());
                } while (article.QteStock < qte && article.QteStock > 0 && ++attempts < maxAttempts);

                if (attempts >= maxAttempts)
                {
                    Console.WriteLine("Too many attempts. Operation canceled.");
                    return null;
                }
                if (article.QteStock == 0)
                {
                    Console.WriteLine("Out of stock. Please select another article.");
                    continue;
                }

                DetailDette detail = DoesExist(dette, article);
                if (detail != null)
                {
                    detail.Qte += qte;
                    article.QteStock -= qte;
                }
                else
                {
                    dette.MontantVerser = 0;
                    DetailDette detailDette = new DetailDette
                    {
                        Article = article,
                        Qte = qte,
                        Dette = dette
                    };
                    article.QteStock -= qte;
                    dette.SetdetailDettes(detailDette);
                    if (UserConnected.getUserConnected().Role == Role.BOUTIQUIER)
                    {
                        articleService.Update(article);
                    }
                    dette.StateDette = StateDette.DESARCHIVER;
                    dette.Client = client;
                    client.setDette(dette);
                }

                if (Resp() != 1)
                    break;
            }

            if (dette.DetailDettes.Count == 0)
                return null;

            dette.MontantRestant = dette.Montant;

            return dette;
        }

        private int Resp()
        {
            int rep;
            do
            {
                Console.WriteLine("Would you like to add another article? 1-Yes 2-No");
                rep = int.Parse(Console.ReadLine());
            } while (rep != 1 && rep != 2);
            return rep;
        }

        public DetailDette DoesExist(Dette dette, Article article)
        {
            foreach (var detail in dette.DetailDettes)
            {
                if (detail.Article.Id == article.Id)
                {
                    return detail;
                }
            }
            return null;
        }

        public void FindDette(string tel)
        {
            Client client = clientService.findByTelephone(tel);
            if (client == null)
            {
                Console.WriteLine("Client does not exist");
                return;
            }

            List<Dette> list = AfficherDettesValides(client);

            if (list.Count == 0)
            {
                Console.WriteLine("No debts found");
                return;
            }

            int rep;
            do
            {
                rep = MenuOptions();
                switch (rep)
                {
                    case 1:
                        AfficherInfosDette(client);
                        break;
                    case 2:
                        AfficherDetailsDette(client);
                        break;
                    case 3:
                        AfficherToutesLesDettes(client);
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            } while (rep != 4);
        }

        private List<Dette> AfficherDettesValides(Client client)
        {
            List<Dette> list = detteService.ListDetEc(client);
            List<Dette> dets = new List<Dette>();

            Console.WriteLine("Mes dettes :");
            foreach (var dette in list)
            {
                if (dette.EtatDette == EtatDette.VALIDER && dette.MontantRestant > 0)
                {
                    dets.Add(dette);
                }
            }
            return dets;
        }

        private int MenuOptions()
        {
            Console.WriteLine("Voulez-vous ?");
            Console.WriteLine("1-Voir les infos d'une dette");
            Console.WriteLine("2-Voir les détails de la dette");
            Console.WriteLine("3-Voir toutes mes dettes");
            Console.WriteLine("4-Quitter");
            return int.Parse(Console.ReadLine());
        }

        private void AfficherInfosDette(Client client)
        {
            Console.WriteLine("Enter the dette ID:");
            int id = int.Parse(Console.ReadLine());
            Dette dette = detteService.FindById(id);
            if (dette == null)
            {
                Console.WriteLine("Dette does not exist");
                return;
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Dette information:");
            Console.WriteLine($"ID: {dette.Id}");
            Console.WriteLine($"Amount: {dette.Montant}");
            Console.WriteLine($"Remaining Amount: {dette.MontantRestant}");
            Console.WriteLine("--------------------------------------------------");
        }

        private void AfficherDetailsDette(Client client)
        {
            Console.WriteLine("Enter the dette ID:");
            int id = int.Parse(Console.ReadLine());
            Dette dette = detteService.FindById(id);
            if (dette == null)
            {
                Console.WriteLine("Dette does not exist");
                return;
            }

            int choice;
            do
            {
                choice = Ask2See();
                switch (choice)
                {
                    case 1:
                        AfficherArticlesDansDette(dette);
                        break;
                    case 2:
                        AfficherPaiementsDansDette(dette);
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            } while (choice != 3);
        }

        private void AfficherArticlesDansDette(Dette dette)
        {
            List<DetailDette> articles = detteService.findArtInDet(dette);
            foreach (var article in articles)
            {
                Console.WriteLine(article);
            }
        }

        private void AfficherPaiementsDansDette(Dette dette)
        {
            List<Paiement> payments = detteService.ListDetPai(dette);
            foreach (var payment in payments)
            {
                Console.WriteLine(payment);
            }
        }

        private void AfficherToutesLesDettes(Client client)
        {
            List<Dette> dettes = detteService.ListDetEc(client);
            foreach (var dette in dettes)
            {
                Console.WriteLine(dette);
            }
        }

        public int Ask2See()
        {
            Console.WriteLine("1---Show dette articles");
            Console.WriteLine("2---Show dette payments");
            Console.WriteLine("3---Exit");
            return int.Parse(Console.ReadLine());
        }



        public void showDetteWithFilter()
        {
            // Étape 1 : Afficher toutes les dettes
            List<Dette> dettes = detteService.FindAll();
            displayDettes(dettes);

            // Étape 2 : Filtrer les dettes si l'utilisateur le souhaite
            if (askForFilter())
            {
                dettes = filterDettesByState();
                displayDettes(dettes);
            }

            // Étape 3 : Demander si l'utilisateur souhaite changer l'état d'une dette
            if (askForStateChange())
            {
                changeDetteState();
            }

        }

        public void listDemandeDette(Client client)
        {
            if (client == null)
            {
                Console.WriteLine("Client non existant COMMENT CA");
                return;
            }
            List<Dette> dettes = detteService.ListDetEc(client);
            displayDettes(dettes);
            Console.WriteLine("Filtrer vos dettes ? 1-Oui 0-Non");
            int rep = Convert.ToInt32(Console.ReadLine());
            if (rep == 1)
            {
                EtatDette etat = choiceFilter();
                foreach (var dette in dettes)
                {
                    Console.WriteLine(dette.EtatDette);
                    if (dette.EtatDette.CompareTo(etat) == 0)
                    {
                        Console.WriteLine(dette);
                    }
                }
            }

        }

        public EtatDette choiceFilter()
        {
            Console.WriteLine("Voulez vous les filtrer par 1-Encours 2-ANNULER 3-VALIDER");
            int rep = Convert.ToInt32(Console.ReadLine());
            EtatDette etat = EtatDette.ENCOURS;
            do
            {
                switch (rep)
                {
                    case 1:
                        etat = EtatDette.ENCOURS;
                        break;
                    case 2:
                        etat = EtatDette.ANNULER;
                        break;
                    case 3:
                        etat = EtatDette.VALIDER;
                        break;
                    default:
                        break;
                }
            } while (rep != 1 && rep != 2 && rep != 3);
            return etat;
        }

        // Fonction pour afficher toutes les dettes
        private List<Dette> displayDettes(List<Dette> dettes)
        {

            foreach (var dette in dettes)
            {
                Console.WriteLine(dette);
            }
            return dettes;
        }

        // Fonction pour demander à l'utilisateur s'il souhaite filtrer les dettes
        private bool askForFilter()
        {
            Console.WriteLine("Voulez vous les filtrer ?");
            Console.WriteLine("1---Oui");
            Console.WriteLine("2---Non");
            int rep = Convert.ToInt32(Console.ReadLine());
            return rep == 1;
        }

        // Fonction pour filtrer les dettes par état (EN_COURS ou ANNULER)
        private List<Dette> filterDettesByState()
        {
            EtatDette etat = choiceFilter();
            return detteService.ListDetByEtat(etat);
        }

        // Fonction pour demander à l'utilisateur s'il souhaite changer l'état d'une
        // dette
        private bool askForStateChange()
        {
            Console.WriteLine("Voulez vous changer l'etat d'une dette ?");
            Console.WriteLine("1---Oui");
            Console.WriteLine("2---Non");
            int rep = Convert.ToInt32(Console.ReadLine());
            return rep == 1;
        }

        // Fonction pour changer l'état d'une dette spécifique
        private void changeDetteState()
        {
            Console.WriteLine("Voulez vous changer l'etat d'une dette a 1-VALIDER 2-REFUSER");
            int rep = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Entrer l'id de la dette");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Entrer le numero de telephone du client");
            String tel = Console.ReadLine();

            // Vérifier l'existence du client
            Client client = clientService.findByTelephone(tel);
            if (client == null)
            {
                Console.WriteLine("Client non existant");
                return;
            }

            // Vérifier l'existence de la dette
            Dette dette = detteService.FindById(id);
            if (dette == null || dette.EtatDette.Equals(EtatDette.VALIDER))
            {
                Console.WriteLine("Dette non existante");
                return;
            }

            // Changer l'état de la dette en fonction de la sélection de l'utilisateur
            EtatDette etat = (rep == 1) ? EtatDette.VALIDER : EtatDette.ANNULER;
            dette.EtatDette = etat;
            if (dette.EtatDette.Equals(EtatDette.VALIDER))
            {
                Console.WriteLine("hii");
                List<DetailDette> details = detteService.findArtInDet(dette);
                foreach (var detail in details)
                {
                    Console.WriteLine("hii");
                    articleService.Update(detail.Article);
                }
            }

            detteService.Update(dette);
        }
    }
}


