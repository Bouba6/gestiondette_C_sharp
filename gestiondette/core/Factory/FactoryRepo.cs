using gestiondette.Core;
using gestiondette.entities;
using gestiondette.Enum;
using gestiondette.Repository.Bd;
using gestiondette.Repository.List;
using gestiondette.Repository.Bd1;


namespace gestiondette.core.Factory
{
    public static class FactoryRepo<T>
    {
        public static object CreateRepository(Persistence persistence)
        {
            switch (persistence)
            {
                case Persistence.List:
                    if (typeof(T) == typeof(User))
                    {
                        return (IRepository<T>)new UserRepositoryListImpl();
                    }
                    else if (typeof(T) == typeof(Client))
                    {
                        return (IRepository<T>)new ClientRepositoryListImpl();
                    }
                    else if (typeof(T) == typeof(Article))
                    {
                        return (IRepository<T>)new ArticleRepositoryListImpl();

                    }
                    else if (typeof(T) == typeof(Dette))
                    {
                        return (IRepository<T>)new DetteRepositoryListImpl();
                    }
                    else if (typeof(T) == typeof(DetailDette))
                    {
                        return (IRepository<T>)new DetailRepositoryListImpl();
                    }
                    else if (typeof(T) == typeof(Paiement))
                    {
                        return (IRepository<T>)new PaiementRepositoryListImpl();
                    }
                    else
                    {
                        return null;
                    }
                case Persistence.Bd:
                    // Retourne un seul repository basé sur la base de données
                    if (typeof(T) == typeof(Client))
                    {
                        return (IRepository<T>)new ClientRepositoryBdImpl(new DatabaseConnexion(), new UserRepositoryBdImpl(new DatabaseConnexion()));
                    }
                    else if (typeof(T) == typeof(User))
                    {
                        return (IRepository<T>)new UserRepositoryBdImpl(new DatabaseConnexion());
                    }
                    else if (typeof(T) == typeof(Article))
                    {
                        return (IRepository<T>)new ArticleRepositoryBdImpl(new DatabaseConnexion());
                    }
                    else if (typeof(T) == typeof(Dette))
                    {
                        return (IRepository<T>)new DetteRepositoryBdImpl(new DatabaseConnexion(), new DetailRepositoryBdImpl(new DatabaseConnexion()));
                    }
                    else if (typeof(T) == typeof(DetailDette))
                    {
                        return (IRepository<T>)new DetailRepositoryBdImpl(new DatabaseConnexion());
                    }
                    else if (typeof(T) == typeof(Paiement))
                    {
                        return (IRepository<T>)new PaiementRepositoryBdImpl(new DatabaseConnexion());
                    }
                    else
                    {
                        return null;
                    }

                case Persistence.Bd1:
                    {
                        if (typeof(T) == typeof(User))
                        {
                            return (IRepository<T>)new UserRepositoryBd1Impl();
                        }
                        else if (typeof(T) == typeof(Client))
                        {
                            return (IRepository<T>)new ClientRepositoryBd1Impl();
                        }
                        else
                        {
                            return null;
                        }
                    }


                default:
                    throw new System.Exception("Persistence not supported");
            }
        }
    }
}
