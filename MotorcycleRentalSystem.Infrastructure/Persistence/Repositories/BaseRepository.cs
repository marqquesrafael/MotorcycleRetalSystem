using MotorcycleRentalSystem.Domain.Entities;
using MotorcycleRentalSystem.Domain.Interfaces.Repositories;
using MotorcycleRentalSystem.Infrastructure.Configuration;

namespace MotorcycleRentalSystem.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected MotorcycleRentalSystemDbContext _DbContext;

        public BaseRepository(MotorcycleRentalSystemDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public void Insert(TEntity entity)
        {
            entity.CreatedAt = DateTime.UtcNow;

            _DbContext.Add(entity);
            _DbContext.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _DbContext.Set<TEntity>().Remove(entity);
            _DbContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            _DbContext.Set<TEntity>().Update(entity);
            _DbContext.SaveChanges();
        }

        public IQueryable<TEntity> Select() => _DbContext.Set<TEntity>();

        public TEntity GetById(long id) => _DbContext.Set<TEntity>().Find(id);

        public void RemoveSoftly(TEntity entity)
        {
            entity.Active = false;
            Update(entity);
        }
    }
}
