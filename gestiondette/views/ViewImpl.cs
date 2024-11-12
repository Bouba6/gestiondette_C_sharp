using gestiondette.core;
namespace gestiondette.Views
{

    public abstract class ViewImpl<T> : IView<T>
    {
        public void afficher(List<T> list)
        {

            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public T saisie()
        {
            return default(T);
        }
    }
}