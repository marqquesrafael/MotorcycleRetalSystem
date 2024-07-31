using MotorcycleRentalSystem.Domain.Entities.User;

namespace MotorcycleRentalSystem.Domain.Entities.Motorcycle
{
    public class CurrentYearMotorcycleEntity : BaseEntity
    {
        public CurrentYearMotorcycleEntity()
        {

        }

        public CurrentYearMotorcycleEntity(long createdBy, long motorcycleId)
        {
            CreatedBy = createdBy;
            MotorcycleId = motorcycleId;
        }

        public long CreatedBy { get; set; }
        public long MotorcycleId { get; set; }

        public virtual UserEntity User { get; set; }
        public virtual MotorcycleEntity Motorcycle { get; set; }
    }
}
