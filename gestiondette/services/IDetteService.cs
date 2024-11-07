using gestiondette.entities;
using gestiondette.Core;
using gestiondette.Enum;
namespace gestiondette.Services
{
    public interface IDetteService : IService<Dette>
    {

        public List<Dette> ListDetEc(Client client);

        public List<DetailDette> findArtInDet(Dette dette);

        public List<Paiement> ListDetPai(Dette dette);

        public List<Dette> ListDetByEtat(EtatDette etat);

    }
}