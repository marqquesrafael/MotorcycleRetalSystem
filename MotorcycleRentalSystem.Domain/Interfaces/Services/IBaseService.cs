using MotorcycleRentalSystem.Domain.Entities;

namespace MotorcycleRentalSystem.Domain.Interfaces.Services
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        void Create(TEntity entity);

        void Delete(long id);

        void Update(TEntity entity);

        IQueryable<TEntity> GetAll();

        TEntity GetById(long id);

        void RemoveSoftly(long id);
    }
}
