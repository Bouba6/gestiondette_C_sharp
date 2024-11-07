using gestiondette.core;
namespace gestiondette.Views
{

    public class ViewImpl<T> : IView<T>
    {
        public void afficher(List<T> list)
        {
            // Loop through the list and display each element
            foreach (var item in list)
            {
                Console.WriteLine(item.ToString());  // Assuming T has a meaningful ToString() method
            }
        }

        public T saisie()
        {
            return default(T);
        }
    }
}