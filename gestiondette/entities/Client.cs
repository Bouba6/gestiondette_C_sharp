using System.ComponentModel.DataAnnotations.Schema;

namespace gestiondette.entities
{
    public class Client : AbstractEntity
    {

        private readonly List<Dette> listDette = [];



        public string? Surnom { get; set; }
        public string? Telephone { get; set; }
        public string? Adresse { get; set; }

        public double Solde { get; set; }



        public User? User { get; set; }

        public Dette ListDette { get => ListDette; }



        public void setDette(Dette dette)
        {
            listDette.Add(dette);
        }



        public override string ToString()
        {
            return "Client[" +
                    "id=" + Id +
                    ", surnom='" + Surnom + '\'' +
                    ", telephone='" + Telephone + '\'' +
                    ", adresse='" + Adresse + ']';

        }


    }
}