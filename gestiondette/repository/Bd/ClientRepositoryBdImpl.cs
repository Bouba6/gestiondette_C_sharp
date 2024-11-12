
using Dapper;
using gestiondette.core;
using gestiondette.Core;
using gestiondette.entities;
using Npgsql;


namespace gestiondette.Repository.Bd
{


    public class ClientRepositoryBdImpl : IRepositoryImpl<Client>, IClientRepository
    {
        private DatabaseConnexion databaseConnexion;
        private UserRepositoryBdImpl userRepositoryBdImpl;
        public ClientRepositoryBdImpl(DatabaseConnexion databaseConnexion, UserRepositoryBdImpl userRepositoryBdImpl) : base(databaseConnexion)
        {
            this.databaseConnexion = databaseConnexion;
            this.userRepositoryBdImpl = userRepositoryBdImpl;
        }
        public void Insert(Client entity)
        {
            using (var connection = databaseConnexion.OpenConnection())
            {

                string query = @"INSERT INTO client (nom, adresse, solde, telephone, ""userId"") 
                         VALUES (@Nom, @Adresse, @Solde, @Telephone, @UserId) RETURNING id;";

                var parameters = new
                {
                    Nom = entity.Surnom,
                    Adresse = entity.Adresse,
                    Solde = entity.Solde,
                    Telephone = entity.Telephone,
                    UserId = entity.User != null ? (int?)entity.User.Id : null  // Conditionally set userId
                };

                entity.Id = connection.ExecuteScalar<int>(query, parameters);
            }
        }


        public void SelectAll()
        {
            base.SelectAll();
        }
        protected override List<string> GetExcludedFieldsInsert()
        {
            return new List<string>() { "Id", "ListDette", "UpdateAt", "UserUpdate", "UserCreate" };
        }
        protected override string GetColumns()
        {
            return "id,nom,telephone,adresse,solde";
        }

        protected override string GetTableName()
        {
            return "client";
        }

        public Client getByTelephone(string telephone)
        {
            using (var connection = databaseConnexion.OpenConnection())
            {
                string columns = GetColumns();
                string query = $@"SELECT {columns} FROM client WHERE telephone = @Telephone LIMIT 1";
                return connection.QueryFirstOrDefault<Client>(query, new { Telephone = telephone });
            }
        }



        public Client getClientByUser(User user)
        {
            using (var connection = databaseConnexion.OpenConnection())
            {

                string query = @"SELECT * 
                         FROM client 
                         WHERE ""userid"" = @User";
                return connection.QueryFirstOrDefault<Client>(query, new { UserId = user.Id });
            }
        }


    }
}