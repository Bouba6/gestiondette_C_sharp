using gestiondette.Enum;

namespace gestiondette.entities
{
    public class Dette
    {
        private static int count = 1; // Initialize count to start from 1

        private int id;
        private double montant;
        private Client client;
        private List<DetailDette> detailDettes = new List<DetailDette>();
        private double montantRestant;
        private double montantVerser;
        private EtatDette etatDette;
        private List<Paiement> paiements = new List<Paiement>();

        private StateDette stateDette;

        // Constructor
        public Dette()
        {
            id = count++;
        }

        // Properties
        public int Id { get => id; set => id = value; }
        public Client Client { get => client; set => client = value; }
        public double MontantRestant { get => montantRestant; set => montantRestant = value; }
        public double MontantVerser { get => montantVerser; set => montantVerser = value; }
        public List<DetailDette> DetailDettes { get => detailDettes; }
        public EtatDette EtatDette { get => etatDette; set => etatDette = value; }
        public double Montant { get => montant; set => montant = value; }
        public List<Paiement> Paiements { get => paiements; }

        public StateDette StateDette { get => stateDette; set => stateDette = value; }

        // Methods
        public void SetdetailDettes(DetailDette detailDette)
        {
            montant += detailDette.Qte * detailDette.Article.Prix;
            detailDettes.Add(detailDette);
        }

        public void SetPaiements(Paiement paiement)
        {
            montantVerser += paiement.Montant;
            MontantRestant -= paiement.Montant;
            paiements.Add(paiement);
        }

        public override string ToString()
        {
            return "Dette[" +
                    "id=" + id +
                    ", montant=" + montant +
                    ", montantRestant=" + montantRestant +
                    ", montantVerser=" + montantVerser +
                    ", etatDette=" + etatDette +
                    ']';
        }
    }
}
