
using gestiondette.Enum;

namespace gestiondette.entities
{
    public class User
    {
        private int id;

        private string login;

        private string password;

        private Role role;


        private Client client;

        private static int count;

        private bool state;

        public User()
        {
            count++;
            id = count;
            state = true;
        }

        public int Id { get => id; set => id = value; }
        public string Login { get => login; set => login = value; }
        public string Password { get => password; set => password = value; }
        public Role Role { get => role; set => role = value; }

        public Client Client { get => client; set => client = value; }
        public bool State { get => state; set => state = value; }



        public override string ToString()
        {
            return "Client[" +
                    "id=" + id +
                    ", login='" + login + '\'' +
                    ", password='" + password + '\'' +
                    ", role=" + role +
                    ']';

        }

    }
}