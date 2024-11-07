using gestiondette.Core;
using gestiondette.core;
using gestiondette.Services;
using gestiondette.Services.Impl;
using gestiondette.Enum;
using gestiondette.Views;
using gestiondette.views;
using gestiondette.entities;

public class PaiementView : ViewImpl<Paiement>
{
    private IClientService clientService;
    private IDetteService detteService;

    public PaiementView(IClientService clientService, IDetteService detteService)
    {
        this.clientService = clientService;
        this.detteService = detteService;

    }


    public Paiement saisie()
    {
        do
        {

            Paiement paiement = new Paiement();
            Client client = findClientByPhone();
            if (client == null)
            {
                Console.WriteLine("Client non existant");
                return null;
            }

            List<Dette> dettes = detteService.ListDetEc(client);
            Console.WriteLine("------------------------------------");
            Console.WriteLine(client);
            Console.WriteLine("------------------------------------");
            foreach (var dette1 in dettes)
            {
                Console.WriteLine(dette1);
            }
            Console.WriteLine("------------------------------------");
            if (dettes.Count == 0)
            {
                Console.WriteLine("Aucune dette");
                return null;
            }

            Dette dette = findDetteById(client);
            if (dette == null || dette.Montant == 0)
            {

                return null;
            }
            if (dette.MontantRestant == 0 || dette.StateDette == StateDette.ARCHIVER)
            {
                Console.WriteLine("La dette est totalement payée");
                // dette.setState(StateDette.ARCHIVER);
                // detteService.update(dette);
                return null;
            }

            if (dette.EtatDette != EtatDette.VALIDER)
            {
                Console.WriteLine("Cette dette n'est pas encore validee");
                return null;
            }

            double montant = promptMontant(dette);
            if (montant == dette.MontantRestant)
            {
                dette.StateDette = StateDette.ARCHIVER;
                detteService.Update(dette);
            }
            paiement.Montant = montant;
            paiement.Dette = dette;

            dette.SetPaiements(paiement);
            detteService.Update(dette);
            return paiement;
        } while (askContinue() == 1);
    }

    // 1. Trouver le client par téléphone
    private Client findClientByPhone()
    {
        Console.WriteLine("Entrer le numero de telephone du client");
        String tel = Console.ReadLine();
        return clientService.findByTelephone(tel);
    }

    // 2. Afficher les dettes d'un client

    // 3. Trouver la dette par ID
    // 3. Trouver la dette par ID en évitant une boucle infinie
    private Dette findDetteById(Client client)
    {
        Dette dette = null;
        int id;
        int attempts = 0;
        int maxAttempts = 3;
        do
        {
            Console.WriteLine("Entrer l'id de la dette (ou tapez -1 pour quitter)");
            id = Convert.ToInt32(Console.ReadLine());

            if (id == -1)
            {
                Console.WriteLine("Opération annulée par l'utilisateur.");
                return null;
            }
            dette = detteService.FindById(id);

            if (dette == null)
            {
                Console.WriteLine("Dette non existante.");
            }
            else if (dette.MontantRestant == 0)
            {
                Console.WriteLine("La dette est déjà payée.");
                return null;
            }

            attempts++;

        } while (dette == null && attempts < maxAttempts);

        if (attempts >= maxAttempts)
        {
            Console.WriteLine("Trop d'essais. Opération annulée.");
            return null;
        }

        return dette;
    }

    // 4. Saisir le montant à payer
    private double promptMontant(Dette dette)
    {
        double montant;
        do
        {
            Console.WriteLine("La dette est de : " + dette.MontantRestant);
            Console.WriteLine("Entrer le montant a payer");
            montant = Convert.ToDouble(Console.ReadLine());

        } while (montant > dette.MontantRestant);
        return montant;
    }

    // 5. Demander si on continue ou non
    private int askContinue()
    {
        int rep;
        do
        {
            Console.WriteLine("Voulez-vous continuer ? 1-oui 2-non");
            rep = Convert.ToInt32(Console.ReadLine());
        } while (rep != 1 && rep != 2);
        return rep;
    }
}
