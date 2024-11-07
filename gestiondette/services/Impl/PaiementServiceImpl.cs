

using gestiondette.entities;
using gestiondette.Repository;

namespace gestiondette.Services.Impl
{
    public class PaiementServiceImpl : IPaiementService
    {
        private readonly IPaiementRepository paiementRepository;

        public PaiementServiceImpl(IPaiementRepository paiementRepository)
        {
            this.paiementRepository = paiementRepository;
        }

        public List<Paiement> FindAll()
        {
            return paiementRepository.SelectAll();
        }

        public Paiement FindById(int id)
        {
            return paiementRepository.SelectById(id);
        }

        public void Save(Paiement paiement)
        {
            paiementRepository.Insert(paiement);
        }

        public void Delete(int id)
        {
            paiementRepository.Delete(id);
        }

        public void Update(Paiement paiement)
        {
            paiementRepository.Update(paiement);
        }
    }
}