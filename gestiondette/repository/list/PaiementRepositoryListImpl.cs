using gestiondette.entities;

namespace gestiondette.Repository.List
{

    public class PaiementRepositoryListImpl : IPaiementRepository
    {
        private readonly List<Paiement> paiements = new List<Paiement>();

        public List<Paiement> SelectAll()
        {
            return paiements;
        }
        public Paiement SelectById(int id)
        {
            foreach (var paiement in paiements)
            {
                if (paiement.Id == id)
                    return paiement;
            }
            return null;
        }
        public void Insert(Paiement client)
        {
            paiements.Add(client);
        }

        public void Update(Paiement paiement)
        {
            int position = paiements.FindIndex(cl => cl.Id == paiement.Id);
            if (position != -1)
                paiements[position] = paiement;
        }
        /// <summary>
        /// Delete an article by its ID
        /// </summary>
        /// <param name="id">The ID of the article to delete</param>
        public void Delete(int id)
        {
            Paiement clientToRemove = paiements.Find(cl => cl.Id == id);
            if (clientToRemove != null)
                paiements.Remove(clientToRemove);
        }
    }
}