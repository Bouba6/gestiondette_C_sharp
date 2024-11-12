
using Dapper;
using gestiondette.core;
using gestiondette.Core;
using gestiondette.entities;
using gestiondette.Enum;
using Npgsql;

using gestiondette.Repository.Bd;
namespace gestiondette.Repository.Bd
{


    public class DetteRepositoryBdImpl : IRepositoryImpl<Dette>, IdetteRepositorie
    {
        private DatabaseConnexion databaseConnexion;
        private DetailRepositoryBdImpl detailDetteRepositoryBdImpl;
        public DetteRepositoryBdImpl(DatabaseConnexion databaseConnexion, DetailRepositoryBdImpl detailDetteRepositoryBdImpl) : base(databaseConnexion)
        {
            this.databaseConnexion = databaseConnexion;
            this.detailDetteRepositoryBdImpl = detailDetteRepositoryBdImpl;
        }

        public void Insert(Dette entity)
        {

            using (var connection = databaseConnexion.OpenConnection())
            {
                var query = "INSERT INTO dette (montantrestant,etatid,montant,clientId,stateid,montantverser) VALUES (@MontantRestant,@EtatDette,@Montant,@Client,@StateDette,@MontantVerser) RETURNING id";
                Console.WriteLine(query);


                // Log the query with actual values
                Console.WriteLine("Executing query: " + query);
                Console.WriteLine("With parameters:");
                Console.WriteLine($"Montant: {entity.Montant}");
                Console.WriteLine($"MontantRestant: {entity.MontantRestant}");
                Console.WriteLine($"MontantVerser: {entity.MontantVerser}");
                Console.WriteLine($"EtatDette: {entity.EtatDette}");
                Console.WriteLine($"StateDette: {entity.EtatDette}");
                Console.WriteLine($"ClientId: {entity.Client.Id}");


                var Params = new
                {
                    Montant = entity.Montant,
                    MontantRestant = entity.MontantRestant,
                    MontantVerser = entity.MontantVerser,
                    EtatDette = entity.EtatDette,
                    StateDette = entity.EtatDette,
                    Client = entity.Client.Id // Assurez-vous d'avoir l'ID du Client et non l'objet complet
                };
                entity.Id = connection.ExecuteScalar<int>(query, Params);
                foreach (var detailDette in entity.DetailDettes)
                {
                    Console.WriteLine($"dette: {entity}");
                    detailDette.Dette = entity;
                    detailDetteRepositoryBdImpl.Insert(detailDette);
                }

            }
        }

        public List<DetailDette> ListDetArt(Dette dette)
        {
            throw new NotImplementedException();
        }

        public List<Dette> ListDetEc(Client client)
        {
            throw new NotImplementedException();
        }

        public List<Paiement> ListDetPai(Dette dette)
        {
            throw new NotImplementedException();
        }

        public List<Dette> showByEtat(EtatDette etat)
        {
            throw new NotImplementedException();
        }
    }
}