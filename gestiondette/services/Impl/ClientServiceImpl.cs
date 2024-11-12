
using gestiondette.entities;
using gestiondette.Repository;

namespace gestiondette.Services.Impl
{
    public class ClientServiceImpl : IClientService
    {
        private readonly IClientRepository clientRepository;

        public ClientServiceImpl(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public List<Client> FindAll()
        {
            return clientRepository.SelectAll();
        }

        public Client FindById(int id)
        {
            return clientRepository.SelectById(id);
        }

        public void Save(Client client)
        {
            Console.WriteLine(client);
            if (client == null)
            {
                Console.WriteLine("c'est nulle");
            }
            client?.OnPrePersist();
            clientRepository.Insert(client);
        }

        public void Delete(int id)
        {
            clientRepository.Delete(id);
        }

        public void Update(Client client)
        {
            clientRepository.Update(client);
        }

        public Client findByTelephone(string telephone)
        {
            return clientRepository.getByTelephone(telephone);
        }

        public Client getClientByUser(User user)
        {
            return clientRepository.getClientByUser(user);
        }
    }
}