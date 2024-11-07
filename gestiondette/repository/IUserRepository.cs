
using gestiondette.Core;
using gestiondette.entities;
using gestiondette.Enum;
namespace gestiondette.Repository
{
    public interface IUserRepository : IRepository<User>
    {

        User findByLogin(string login, string password);
        List<User> findByRole(Role role);
        List<User> findByState(bool state);
    }
}