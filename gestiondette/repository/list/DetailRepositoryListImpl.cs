using gestiondette.entities;

namespace gestiondette.Repository.List
{

    public class DetailRepositoryListImpl : IdetailRepository
    {
        private readonly List<DetailDette> detailDettes = new List<DetailDette>();

        public List<DetailDette> SelectAll()
        {
            return detailDettes;
        }
        public DetailDette SelectById(int id)
        {
            foreach (var detailDette in detailDettes)
            {
                if (detailDette.Id == id)
                    return detailDette;
            }
            return null;
        }
        public void Insert(DetailDette detailDette)
        {
            detailDettes.Add(detailDette);
        }
        public void Update(DetailDette detailDette)
        {
            int position = detailDettes.FindIndex(cl => cl.Id == detailDette.Id);
            if (position != -1)
                detailDettes[position] = detailDette;
        }
        public void Delete(int id)
        {
            DetailDette clientToRemove = detailDettes.Find(cl => cl.Id == id);
            if (clientToRemove != null)
                detailDettes.Remove(clientToRemove);
        }
    }
}