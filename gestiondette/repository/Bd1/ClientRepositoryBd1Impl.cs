using GesDette.Core.Db;
using gestiondette.entities;
using gestiondette.Enum;
namespace gestiondette.Repository.Bd1
{
    public class ClientRepositoryBd1Impl : IClientRepository
    {
        public void Delete(int id)
        {
            using (var context = new GesDetteContext())
            {
                var client = context.Client.Find(id);
                context.Client.Remove(client);
                context.SaveChanges();
            }
        }

        public Client getByTelephone(string telephone)
        {
            using (var context = new GesDetteContext())
            {
                return context.Client.FirstOrDefault(c => c.Telephone == telephone);
            }
        }

        public Client getClientByUser(User user)
        {
            using (var context = new GesDetteContext())
            {
                return context.Client.FirstOrDefault(c => c.User.Id == user.Id);
            }
        }

        public void Insert(Client entity)
        {
            using (var context = new GesDetteContext())
            {
                Console.WriteLine(entity.User.ToString());
                context.Client.Add(entity);
                context.SaveChanges();
            }
        }

        public List<Client> SelectAll()
        {
            using (var context = new GesDetteContext())
            {
                return context.Client.ToList();
            }
        }

        public Client SelectById(int id)
        {
            using (var context = new GesDetteContext())
            {
                return context.Client.FirstOrDefault(c => c.Id == id);
            }
        }

        public void Update(Client entity)
        {
            using (var context = new GesDetteContext())
            {
                context.Client.Update(entity);
                context.SaveChanges();
            }
        }
    }
}