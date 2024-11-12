
using Npgsql;
using System.Data;
using gestiondette.core;
// Add the missing namespace for IDataBaseConnexion//+
namespace gestiondette.core
{
    public class DatabaseConnexion : IDataBaseConnexion
    {
        private readonly string connexionString = "Host=localhost;Port=5433;Database=gesdette2;Username=postgres;Password=root;";
        protected NpgsqlConnection? connexion;


        // Méthode pour fermer la connexion
        public void CloseConnection()
        {
            if (connexion != null && connexion.State == ConnectionState.Open)
            {
                try
                {
                    connexion.Close();
                    connexion = null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la fermeture de la connexion : {ex.Message}");
                }
            }
        }

        // Méthode pour ouvrir la connexion
        public NpgsqlConnection OpenConnection()
        {
            try
            {
                connexion = new NpgsqlConnection(connexionString);
                connexion.Open();
                return connexion;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'ouverture de la connexion : {ex.Message}");
                throw; // Rethrow l'exception après avoir loggé l'erreur
            }
        }
    }
}
