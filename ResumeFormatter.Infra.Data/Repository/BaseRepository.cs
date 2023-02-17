using ResumeFormatter.Domain.Entities;
using ResumeFormatter.Domain.Interfaces.Repository;

namespace ResumeFormatter.Infra.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(TEntity obj)
        {
            throw new NotImplementedException();
        }

        public IList<TEntity> Select()
        {
            throw new NotImplementedException();
        }

        public TEntity Select(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(TEntity obj)
        {
            throw new NotImplementedException();
        }
    }
}
