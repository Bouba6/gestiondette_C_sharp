using gestiondette.entities;
using gestiondette.Core;
namespace gestiondette.Services
{
    public interface IClientService : IService<Client>
    {

        public Client findByTelephone(string telephone);

        public Client getClientByUser(User user);

    }
}