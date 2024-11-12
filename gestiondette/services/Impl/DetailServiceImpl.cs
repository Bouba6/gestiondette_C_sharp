
using gestiondette.entities;
using gestiondette.Repository;

namespace gestiondette.Services.Impl
{
    public class DetailServiceImpl : IdetailService
    {
        private readonly IdetailRepository detailrepo;

        public DetailServiceImpl(IdetailRepository detailrepo)
        {
            this.detailrepo = detailrepo;
        }

        public List<DetailDette> FindAll()
        {
            return detailrepo.SelectAll();
        }

        public DetailDette FindById(int id)
        {
            return detailrepo.SelectById(id);
        }

        public void Save(DetailDette detailDette)
        {
            detailDette.CreateAt = DateTime.Now;
            detailrepo.Insert(detailDette);
        }

        public void Delete(int id)
        {
            detailrepo.Delete(id);
        }

        public void Update(DetailDette detailDette)
        {
            detailDette.UpdateAt = DateTime.Now;
            detailrepo.Update(detailDette);
        }
    }
}