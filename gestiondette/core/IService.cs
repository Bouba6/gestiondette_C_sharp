

namespace gestiondette.Core
{
    public interface IService<T>
    {
        List<T> FindAll();
        T FindById(int id);
        void Save(T client);
        void Delete(int id);
        void Update(T client);
    }
}