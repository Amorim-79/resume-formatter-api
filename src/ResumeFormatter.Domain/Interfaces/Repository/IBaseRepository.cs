using ResumeFormatter.Domain.Entities;

namespace ResumeFormatter.Domain.Interfaces.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        bool Insert(TEntity obj);
        bool Update(TEntity obj);
        bool Delete(int id);
        IList<TEntity> Select();
        TEntity Select(int id);
    }
}
