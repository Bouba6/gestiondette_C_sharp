
using Dapper;
using gestiondette.core;
using gestiondette.Core;
using gestiondette.entities;
using Npgsql;


namespace gestiondette.Repository.Bd
{
    public class ArticleRepositoryBdImpl : IRepositoryImpl<Article>, IArticleRepository
    {
        private DatabaseConnexion databaseConnexion;
        public ArticleRepositoryBdImpl(DatabaseConnexion databaseConnexion) : base(databaseConnexion)
        {
            this.databaseConnexion = databaseConnexion;
        }

        protected override string GetColumns()
        {
            return "id,libelle,prix,qtestock";
        }

        public void Insert(Article entity)
        {
            using (var connection = databaseConnexion.OpenConnection())
            {
                string query = @"INSERT INTO article (libelle,prix, qteStock,createat) 
                         VALUES (@Libelle, @Prix, @QteStock, @CreateAt) RETURNING id;";

                var parameters = new
                {
                    Libelle = entity.Libelle,
                    Prix = entity.Prix,
                    QteStock = entity.QteStock,
                    CreateAt = entity.CreateAt
                };
                entity.Id = connection.ExecuteScalar<int>(query, parameters);
            }
        }
        public void Update(Article entity)
        {
            using (var connection = databaseConnexion.OpenConnection())
            {
                string query = @"UPDATE article SET libelle=@Libelle, prix=@Prix, qteStock=@QteStock, updateat=@UpdateAt WHERE id=@Id;";
                var parameters = new
                {
                    Libelle = entity.Libelle,
                    Prix = entity.Prix,
                    QteStock = entity.QteStock,
                    UpdateAt = entity.UpdateAt,
                    Id = entity.Id,
                };
                connection.Execute(query, parameters);

            }
        }


    }
}