
using gestiondette.Core;
using gestiondette.entities;

namespace gestiondette.Repository
{
    public interface IClientRepository : IRepository<Client>
    {

        Client getByTelephone(string telephone);

        Client getClientByUser(User user);

    }
}