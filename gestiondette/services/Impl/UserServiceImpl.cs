
using gestiondette.entities;
using gestiondette.Repository;
using gestiondette.Enum;
namespace gestiondette.Services.Impl
{
    public class UserServiceImpl : IUserService
    {

        private readonly IUserRepository userRepository;

        public UserServiceImpl(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public List<User> FindAll()
        {
            return userRepository.SelectAll();
        }

        public User FindById(int id)
        {
            return userRepository.SelectById(id);
        }

        public void Save(User user)
        {
            user.CreateAt = DateTime.Now;
            userRepository.Insert(user);
        }

        public void Delete(int id)
        {
            userRepository.Delete(id);
        }

        public void Update(User user)
        {
            user.UpdateAt = DateTime.Now;
            userRepository.Update(user);
        }


        public List<User> findByRole(Role role)
        {
            return userRepository.findByRole(role);
        }


        public List<User> findByState(bool etat)
        {

            return userRepository.findByState(etat);
        }

        public User findByLogin(String login, String password)
        {

            return userRepository.findByLogin(login, password);
        }

    }

}