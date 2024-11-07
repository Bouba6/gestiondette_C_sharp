using gestiondette.Core;
using gestiondette.core;
using gestiondette.Services;
using gestiondette.Services.Impl;
using gestiondette.Enum;
using gestiondette.Views;
using gestiondette.views;
using gestiondette.entities;

namespace gestiondette.views
{
    public class ArticleView : ViewImpl<Article>
    {
        IArticleService articleService;

        public ArticleView(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        public Article saisie()
        {
            Article article = new Article();

            Console.WriteLine("Saisir le libelle");
            article.Libelle = Console.ReadLine();
            Console.WriteLine("Saisir le prix");
            article.Prix = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Saisir la quantite en Stock");
            article.QteStock = Convert.ToDouble(Console.ReadLine());
            // User currentUser = UserConnected.getUserConnected();
            // article.setUserCreate(currentUser);
            // article.setCreateAt(LocalDateTime.now());
            // article.onPrePersist();
            // System.out.println(article.getCreateAt());
            return article;
        }

        public void filter()
        {
            List<Article> articles = articleService.FindAll();
            foreach (var article in articles)
            {
                if (article.QteStock != 0)
                {
                    Console.WriteLine(article);
                }
            }
        }

        public Article changeQte()
        {
            Console.WriteLine("entrer l'id de l'article");
            int id = Convert.ToInt32(Console.ReadLine());
            Article article = articleService.FindById(id);
            if (article != null)
            {
                Console.WriteLine(article);
                article.QteStock = askQuantite();

                return article;
            }
            else
            {
                return null;
            }

        }


        private double askQuantite()
        {
            double qte;
            do
            {
                Console.WriteLine("entrer la nouvelle quantite");
                return qte = Convert.ToDouble(Console.ReadLine());
            } while (qte <= 0);
        }



    }

}










