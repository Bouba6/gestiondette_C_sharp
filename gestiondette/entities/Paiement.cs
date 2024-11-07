
namespace gestiondette.entities
{
    public class Paiement
    {
        private int id;

        private double montant;

        private DateTime datePaiement;

        private Dette dette;


        public int Id { get => id; set => id = value; }
        public double Montant { get => montant; set => montant = value; }
        public DateTime DatePaiement { get => datePaiement; set => datePaiement = value; }

        public Dette Dette { get => dette; set => dette = value; }






        public override string ToString()
        {
            return "Client[" +
                    "id=" + id +
                    ", montant='" + montant + '\'' +
                    ", date='" + datePaiement + '\'';
        }

    }



}