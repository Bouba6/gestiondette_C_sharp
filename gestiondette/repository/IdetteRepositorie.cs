
using gestiondette.Core;
using gestiondette.entities;
using gestiondette.Enum;

namespace gestiondette.Repository
{
    public interface IdetteRepositorie : IRepository<Dette>
    {



        List<Dette> ListDetEc(Client client);

        List<DetailDette> ListDetArt(Dette dette);

        List<Paiement> ListDetPai(Dette dette);

        List<Dette> showByEtat(EtatDette etat);


    }
}