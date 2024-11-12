
using System.ComponentModel.DataAnnotations.Schema;
using gestiondette.Enum;

namespace gestiondette.entities
{
    public class User : AbstractEntity
    {


        private bool state;

        public User()
        {

            state = true;
        }


        public string Login { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public Role Role { get; set; }

        [NotMapped]
        public Client Client { get; set; }


        public bool State { get => state; set => state = value; }



        public override string ToString()
        {
            return "User[" +
                    "id=" + Id +
                    ", login='" + Login + '\'' +
                    ", password='" + Password + '\'' +
                    ", role=" + Role +
                    ']';

        }

    }
}