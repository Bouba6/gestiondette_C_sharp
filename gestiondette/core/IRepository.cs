namespace gestiondette.Core
{
    public interface IRepository<T>
    {
        List<T> SelectAll();
        T SelectById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}