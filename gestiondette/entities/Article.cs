namespace gestiondette.entities
{
    public class Article
    {
        private int id;
        private String libelle;

        private double prix;

        private double qteStock;


        private static int count;

        public Article()
        {
            count++;
            id = count;
        }


        public int Id { get => id; set => id = value; }
        public string Libelle { get => libelle; set => libelle = value; }
        public double Prix { get => prix; set => prix = value; }
        public double QteStock { get => qteStock; set => qteStock = value; }



        public override string ToString()
        {
            return "Client[" +
                    "id=" + id +
                    ", libelle='" + libelle + '\'' +
                    ", prix='" + prix + '\'' +
                    ", qteStock='" + qteStock + ']';

        }


    }
}