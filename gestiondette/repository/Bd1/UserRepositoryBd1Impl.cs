using GesDette.Core.Db;
using gestiondette.entities;
using gestiondette.Enum;
namespace gestiondette.Repository.Bd1
{
    public class UserRepositoryBd1Impl : IUserRepository
    {

        public void Delete(int id)
        {
            using (var context = new GesDetteContext())
            {
                var user = context.User.Find(id);
                context.User.Remove(user);
                context.SaveChanges();
            }
        }

        public User findByLogin(string login, string password)
        {
            using (var context = new GesDetteContext())
            {

                var user = context.User.FirstOrDefault(u => u.Login == login);

                // user.ClientId = user.Client.id;

                if (user != null)
                {
                    // Comparer le mot de passe fourni avec le mot de passe haché stocké
                    // if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                    // {
                    //     return user;  // Mot de passe correct
                    // }
                    return user;
                }

                return null;  // Si aucun utilisateur trouvé ou mot de passe incorrec


            }
        }

        public List<User> findByRole(Role role)
        {
            using (var context = new GesDetteContext())
            {
                return context.User.Where(u => u.Role == role).ToList();
            }
        }

        public List<User> findByState(bool state)
        {
            using (var context = new GesDetteContext())
            {
                return context.User.Where(u => u.State == state).ToList();
            }
        }

        public void Insert(User entity)
        {
            using (var context = new GesDetteContext())
            {
                entity.OnPrePersist();

                context.User.Add(entity);
                context.SaveChanges();

                Console.WriteLine("L'ID de l'utilisateur est : " + entity.Id);

            }
        }

        public List<User> SelectAll()
        {
            using (var context = new GesDetteContext())
            {
                return context.User.ToList();
            }
        }

        public User SelectById(int id)
        {
            using (var context = new GesDetteContext())
            {
                return context.User.FirstOrDefault(u => u.Id == id);
            }
        }

        public void Update(User entity)
        {
            using (var context = new GesDetteContext())
            {
                context.User.Update(entity);
                context.SaveChanges();
            }
        }
    }
}