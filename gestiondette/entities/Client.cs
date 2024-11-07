namespace gestiondette.entities
{
    public class Client
    {
        private int id;
        private String surnom;
        private String telephone;
        private String adresse;

        private User user;

        private double solde;

        private List<Dette> listDette = [];

        private static int count;

        public Client()
        {
            count++;
            id = count;
        }


        public int Id { get => id; set => id = value; }
        public string Surnom { get => surnom; set => surnom = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Adresse { get => adresse; set => adresse = value; }

        public double Solde { get => solde; set => solde = value; }
        public User User { get => user; set => user = value; }

        public Dette ListDette { get => ListDette; }

        public void setDette(Dette dette)
        {
            listDette.Add(dette);
        }



        public override string ToString()
        {
            return "Client[" +
                    "id=" + id +
                    ", surnom='" + surnom + '\'' +
                    ", telephone='" + telephone + '\'' +
                    ", adresse='" + adresse + ']';

        }

        public bool equals(Client other)
        {
            if (this == other) return true;
            if (other == null) return false;
            Client client = (Client)other;
            return id == client.id &&
                    Object.Equals(surnom, client.surnom) &&
                    Object.Equals(telephone, client.telephone) &&
                    Object.Equals(adresse, client.adresse);

        }
    }
}