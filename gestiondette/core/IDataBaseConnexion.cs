using Npgsql;

namespace gestiondette.core
{
    public interface IDataBaseConnexion
    {
        void CloseConnection();
        NpgsqlConnection OpenConnection();
    }
}