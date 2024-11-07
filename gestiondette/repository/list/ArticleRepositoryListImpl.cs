using gestiondette.entities;

namespace gestiondette.Repository.List
{

    public class ArticleRepositoryListImpl : IArticleRepository
    {
        private readonly List<Article> articles = new List<Article>();

        public List<Article> SelectAll()
        {
            return articles;
        }
        public Article SelectById(int id)
        {
            foreach (var article in articles)
            {
                if (article.Id == id)
                    return article;
            }
            return null;
        }
        public void Insert(Article client)
        {
            articles.Add(client);
        }
        public void Update(Article article)
        {
            int position = articles.FindIndex(cl => cl.Id == article.Id);
            if (position != -1)
                articles[position] = article;
        }
        public void Delete(int id)
        {
            Article clientToRemove = articles.Find(cl => cl.Id == id);
            if (clientToRemove != null)
                articles.Remove(clientToRemove);
        }
    }
}