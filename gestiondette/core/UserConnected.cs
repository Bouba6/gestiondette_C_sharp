
using gestiondette.entities;

namespace gestiondette.Core
{

    public class UserConnected
    {

        public static User UserConected;

        public static User getUserConnected()
        {
            return UserConected;
        }

        public static void setUserConnected(User UserConnected)
        {
            UserConected = UserConnected;
        }
    }

}