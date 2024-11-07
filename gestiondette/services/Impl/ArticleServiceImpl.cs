
using gestiondette.entities;
using gestiondette.Repository;

namespace gestiondette.Services.Impl
{
    public class ArticleServiceImpl : IArticleService
    {
        private readonly IArticleRepository articleRepository;

        public ArticleServiceImpl(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public List<Article> FindAll()
        {
            return articleRepository.SelectAll();
        }

        public Article FindById(int id)
        {
            return articleRepository.SelectById(id);
        }

        public void Save(Article article)
        {
            articleRepository.Insert(article);
        }

        public void Delete(int id)
        {
            articleRepository.Delete(id);
        }

        public void Update(Article article)
        {
            articleRepository.Update(article);
        }
    }
}