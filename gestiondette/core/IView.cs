
namespace gestiondette.core
{
    public interface IView<T>
    {
        T saisie();

        void afficher(List<T> list);
    }
}