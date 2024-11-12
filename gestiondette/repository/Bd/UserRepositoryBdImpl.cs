
using Dapper;
using gestiondette.core;
using gestiondette.Core;
using gestiondette.entities;
using gestiondette.Enum;
using Npgsql;


namespace gestiondette.Repository.Bd
{


    public class UserRepositoryBdImpl : IRepositoryImpl<User>, IUserRepository
    {
        private DatabaseConnexion databaseConnexion;
        public UserRepositoryBdImpl(DatabaseConnexion databaseConnexion) : base(databaseConnexion)
        {
            this.databaseConnexion = databaseConnexion;
        }
        protected override string GetTableName()
        {
            return "users";
        }

        protected override List<string> GetExcludedFieldsInsert()
        {
            return new List<string>() { "Id", "ListDette", "UpdateAt", "UserUpdate", "UserCreate" };
        }
        protected override string GetColumns()
        {
            return "nom,telephone,adresse,solde";
        }
        public void Insert(User entity)
        {
            using (var connection = databaseConnexion.OpenConnection())
            {
                Console.WriteLine(entity.ToString());
                string query = @"INSERT INTO users (email,etat,login,password,""roleId"") VALUES (@Email,@State,@login,@password,@Role) RETURNING id;";
                Console.WriteLine(query);
                var parameters = new
                {
                    email = entity.Email,
                    State = entity.State ? 1 : 0,  // Convert boolean to integer
                    login = entity.Login,
                    password = entity.Password,
                    Role = entity.Role
                };

                entity.Id = connection.ExecuteScalar<int>(query, parameters);

            }
        }





        public User findByLogin(string login, string password)
        {
            using (var connection = databaseConnexion.OpenConnection())
            {
                string query = @"
                    SELECT u.id, u.login, u.password, r.id AS ""roleId"", r.""nomRole"" AS ""nomRole""
                    FROM users u
                    LEFT JOIN role r ON u.""roleId"" = r.id
                    WHERE u.login = @login AND u.password = @password
                    LIMIT 1"; // LIMIT 1 pour garantir qu'il ne retourne qu'un seul résultat

                // Mappage des résultats pour inclure à la fois User et Role
                var user = connection.Query<User, Role, User>(
                    query,
                    (userResult, roleResult) =>
                    {
                        userResult.Role = roleResult; // Associer le rôle à l'utilisateur
                        return userResult;
                    },
                    new { login, password },
                    splitOn: "roleId" // Dapper saura où séparer les deux objets (User et Role)
                ).FirstOrDefault();

                return user;
            }
        }





        public List<User> findByRole(Role role)
        {
            using (var connection = databaseConnexion.OpenConnection())
            {
                return connection.Query<User>("select * from users where role=@Role", new { role }).ToList();
            }
        }

        public List<User> findByState(bool state)
        {
            using (var connection = databaseConnexion.OpenConnection())
            {
                return connection.Query<User>("select * from users where state=@state", new { state }).ToList();
            }
        }
    }
}