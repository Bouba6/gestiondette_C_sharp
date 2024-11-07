namespace gestiondette.entities
{
    public class DetailDette()
    {
        private int id;
        private double qte;

        private Dette dette;

        private Article article;


        public int Id { get => id; set => id = value; }
        public double Qte { get => qte; set => qte = value; }
        public Dette Dette { get => dette; set => dette = value; }
        public Article Article { get => article; set => article = value; }


        public override string ToString()
        {
            return "Client[" +
                    "id=" + id +
                    ", qte=" + qte +
                    ", article=" + article.Libelle +
                    ']';

        }
    }
}