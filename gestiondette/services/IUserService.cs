using gestiondette.entities;
using gestiondette.Core;
using gestiondette.Enum;
namespace gestiondette.Services
{
    public interface IUserService : IService<User>
    {

        public List<User> findByRole(Role role);


        public List<User> findByState(bool etat);

        public User findByLogin(String login, String password);
    }
}