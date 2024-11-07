using gestiondette.entities;
using gestiondette.Enum;

namespace gestiondette.Repository.List
{

    public class DetteRepositoryListImpl : IdetteRepositorie
    {
        private readonly List<Dette> dettes = new List<Dette>();

        public List<Dette> SelectAll()
        {
            return dettes;
        }
        public Dette SelectById(int id)
        {
            foreach (var dette in dettes)
            {
                if (dette.Id == id)
                    return dette;
            }
            return null;
        }
        public void Insert(Dette dette)
        {
            if (dette != null)
            {
                dettes.Add(dette);
            }

        }
        public void Update(Dette dette)
        {
            int position = dettes.FindIndex(cl => cl.Id == dette.Id);
            if (position != -1)
                dettes[position] = dette;
        }
        public void Delete(int id)
        {
            Dette clientToRemove = dettes.Find(cl => cl.Id == id);
            if (clientToRemove != null)
                dettes.Remove(clientToRemove);
        }



        public List<Dette> ListDetEc(Client client)
        {
            List<Dette> list = [];
            foreach (var dette in dettes)
            {
                if (dette.Client.Id == client.Id)
                    list.Add(dette);

            }
            return list;
        }

        public List<DetailDette> ListDetArt(Dette dette)
        {
            foreach (var dett in dettes)
            {
                if (dett.Id == dette.Id)
                    return dett.DetailDettes;
            }
            return null;
        }

        public List<Paiement> ListDetPai(Dette dette)
        {
            foreach (var dett in dettes)
            {
                if (dett.Id == dette.Id)
                    return dett.Paiements;
            }
            return null;
        }

        public List<Dette> showByEtat(EtatDette etat)
        {
            foreach (var dette in dettes)
            {
                if (dette.EtatDette.CompareTo(etat) == 0)
                    return dettes;
            }
            return null;
        }
    }
}