using Dapper;
using gestiondette.core;
using gestiondette.Core;
using Npgsql;
using System.Data;
using System.Linq;

using gestiondette.entities;

namespace gestiondette.Repository
{
    public class IRepositoryImpl<T> : IRepository<T>
    {
        protected readonly DatabaseConnexion databaseConnexion;

        // Constructeur qui prend la chaîne de connexion pour Dapper
        public IRepositoryImpl(DatabaseConnexion databaseConnexion)
        {
            this.databaseConnexion = databaseConnexion;
        }

        protected virtual string GetTableName()
        {
            return typeof(T).Name.ToLower();
        }

        protected virtual string GetColumns()
        {
            return string.Join(", ", typeof(T).GetProperties().Select(p => p.Name));
        }
        // Delete un enregistrement par son ID
        public void Delete(int id)
        {
            using (var connection = databaseConnexion.OpenConnection())
            {
                string query = $"DELETE FROM {typeof(T).Name.ToLower()} WHERE id = @Id";

                connection.Execute(query, new { Id = id });
            }
        }
        protected virtual List<string> GetExcludedFieldsInsert()
        {
            return new List<string>();
        }

        // Insert une entité dans la base de données
        public void Insert(T entity)
        {
            using (var connection = databaseConnexion.OpenConnection())
            {

            }
        }




        // Sélectionner tous les enregistrements
        public List<T> SelectAll()
        {
            using (var connection = databaseConnexion.OpenConnection())
            {
                string query;
                if (typeof(T) == typeof(User))
                {
                    query = @"
                SELECT u.id, u.login, u.password, r.""nomRole"" AS role
                FROM users u
                JOIN role r ON u.""roleId"" = r.id";
                }
                else
                {
                    query = $"SELECT {GetColumns()} FROM {GetTableName()}";
                }

                Console.WriteLine(query); // Pour vérifier la requête générée
                return connection.Query<T>(query).ToList();
            }
        }


        // Sélectionner un enregistrement par ID
        public T SelectById(int id)
        {
            using (var connection = databaseConnexion.OpenConnection())
            {
                string Get = GetColumns();
                string table = GetTableName();
                string query = $"SELECT {Get} FROM {table} WHERE id = @Id";
                return connection.QueryFirstOrDefault<T>(query, new { Id = id });
            }
        }

        // Mettre à jour une entité dans la base de données
        public void Update(T entity)
        {
            using (var connection = databaseConnexion.OpenConnection())
            {
                var setClause = string.Join(", ", typeof(T).GetProperties()
                    .Where(p => p.Name != "Id") // Exclure la propriété "Id" de l'update (ou la clé primaire)
                    .Select(p => $"{p.Name} = @{p.Name}"));

                var query = $"UPDATE {typeof(T).Name.ToLower()} SET {setClause} WHERE id = @Id";
                connection.Execute(query, entity);
            }
        }
    }
}
