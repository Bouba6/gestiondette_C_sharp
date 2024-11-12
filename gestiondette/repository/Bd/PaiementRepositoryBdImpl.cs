

using gestiondette.core;
using gestiondette.entities;

namespace gestiondette.Repository.Bd
{
    public class PaiementRepositoryBdImpl : IRepositoryImpl<Paiement>, IPaiementRepository
    {
        public PaiementRepositoryBdImpl(DatabaseConnexion databaseConnexion) : base(databaseConnexion)
        {
        }
    }
}