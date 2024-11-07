using gestiondette.entities;
using gestiondette.Enum;
namespace gestiondette.Repository.List
{

    public class UserRepositoryListImpl : IUserRepository
    {
        private readonly List<User> Users = new List<User>();

        public List<User> SelectAll()
        {
            return Users;
        }
        public User SelectById(int id)
        {
            foreach (var User in Users)
            {
                if (User.Id == id)
                    return User;
            }
            return null;
        }
        public void Insert(User user)
        {
            Users.Add(user);
        }
        public void Update(User user)
        {
            int position = Users.FindIndex(us => us.Id == user.Id);
            if (position != -1)
                Users[position] = user;
        }
        public void Delete(int id)
        {
            User userToRemove = Users.Find(cl => cl.Id == id);
            if (userToRemove != null)
                Users.Remove(userToRemove);
        }

        public User findByLogin(string login, string password)
        {
            foreach (var user in Users)
            {
                if (user.Login == login && user.Password == password)
                    return user;
            }
            return null;

        }


        public List<User> findByRole(Role role)
        {

            List<User> users = new List<User>();
            foreach (var user in Users)
            {
                if (user.Role.CompareTo(role) == 0)
                {
                    users.Add(user);
                }

            }
            return users;
        }


        public List<User> findByState(bool state)
        {
            List<User> users = new List<User>();
            foreach (var user in Users)
            {
                if (user.State.CompareTo(state) == 0)
                {
                    users.Add(user);
                }

            }
            return users;
        }


    }
}