
using Dapper;
using gestiondette.core;
using gestiondette.Core;
using gestiondette.entities;
using Npgsql;


namespace gestiondette.Repository.Bd
{


    public class DetailRepositoryBdImpl : IRepositoryImpl<DetailDette>, IdetailRepository
    {
        private DatabaseConnexion databaseConnexion;

        public DetailRepositoryBdImpl(DatabaseConnexion databaseConnexion) : base(databaseConnexion)
        {
            this.databaseConnexion = databaseConnexion;
        }

        public void Insert(DetailDette detailDette)
        {
            using (var connexion = databaseConnexion.OpenConnection())
            {
                string sql = "INSERT INTO detaildette (qte,articleid,detteid) VALUES (@Qte, @Article,@Dette)";
                Console.WriteLine(sql);

                var Params = new
                {
                    Qte = detailDette.Qte,
                    Article = detailDette.Article.Id,
                    Dette = detailDette.Dette.Id
                };

                Console.WriteLine($"Qte: {detailDette.Dette}");
                Console.WriteLine($"Article: {detailDette.Article.Id}");
                Console.WriteLine($"Dette: {detailDette.Dette}");
                connexion.Execute(sql, Params);
            }
        }
    }
}