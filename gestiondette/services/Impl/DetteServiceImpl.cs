
using gestiondette.entities;
using gestiondette.Repository;
using gestiondette.Enum;
namespace gestiondette.Services.Impl
{
    public class DetteServiceImpl : IDetteService
    {
        private readonly IdetteRepositorie repoDette;

        public DetteServiceImpl(IdetteRepositorie repoDette)
        {
            this.repoDette = repoDette;
        }


        public List<Dette> FindAll()
        {
            return repoDette.SelectAll();
        }

        public Dette FindById(int id)
        {
            return repoDette.SelectById(id);
        }

        public void Save(Dette dette)
        {
            if (dette != null)
            {
                dette.CreateAt = DateTime.Now;
                repoDette.Insert(dette);
            }
            Console.WriteLine("dette non enregistrée");

        }

        public void Delete(int id)
        {
            repoDette.Delete(id);
        }

        public void Update(Dette dette)
        {
            dette.UpdateAt = DateTime.Now;
            repoDette.Update(dette);
        }


        public List<Dette> ListDetEc(Client client)
        {
            return repoDette.ListDetEc(client);
        }

        public List<DetailDette> findArtInDet(Dette dette)
        {
            return repoDette.ListDetArt(dette);
        }

        public List<Paiement> ListDetPai(Dette dette)
        {
            return repoDette.ListDetPai(dette);
        }

        public List<Dette> ListDetByEtat(EtatDette etat)
        {
            return repoDette.showByEtat(etat);
        }


    }
}