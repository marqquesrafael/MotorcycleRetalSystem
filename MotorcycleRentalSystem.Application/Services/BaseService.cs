using MotorcycleRentalSystem.Domain.Entities;
using MotorcycleRentalSystem.Domain.Exceptions;
using MotorcycleRentalSystem.Domain.Interfaces.Repositories;
using MotorcycleRentalSystem.Domain.Interfaces.Services;

namespace MotorcycleRentalSystem.Application.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public IQueryable<TEntity> GetAll()
        {
            var entities = _repository.Select();
            return entities;
        }

        public void Create(TEntity entity)
        {
            _repository.Insert(entity);
        }

        public void Delete(long id)
        {
            var entity = GetById(id);

            if (entity is null)
                throw new EntityNotFoundException();

            _repository.Delete(entity);
        }

        public TEntity GetById(long id)
        {
            var entity = _repository.GetById(id);
            return entity;
        }

        public void RemoveSoftly(long id)
        {
            var entity = GetById(id);

            if (entity is null)
                throw new EntityNotFoundException();

            _repository.RemoveSoftly(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity.Id == 0)
                throw new UpdateWithoutIdException();

            _repository.Update(entity);
        }

    }
}
