using MotorcycleRentalSystem.Domain.Entities;

namespace MotorcycleRentalSystem.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        void Insert(TEntity entity);

        void Delete(TEntity entity);

        void RemoveSoftly(TEntity entity);

        void Update(TEntity entity);

        IQueryable<TEntity> Select();

        TEntity GetById(long id);
    }
}
