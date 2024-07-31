using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRentalSystem.Domain.Entities.Motorcycle;
using MotorcycleRentalSystem.Infrastructure.Mapping;

namespace MotorcycleRentalSystem.Infrastructure.Persistence.Mapping.Motorcycle
{
    public class CurrentYearMotorcycleMap : BaseMap<CurrentYearMotorcycleEntity>
    {
        public override string GetTableName() => "tb_current_year_motorcycle";

        public override void Configure(EntityTypeBuilder<CurrentYearMotorcycleEntity> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.CreatedBy)
                .HasColumnName("created_by")
                .IsRequired();

            builder.Property(p => p.MotorcycleId)
                .HasColumnName("motorcycle_id")
                .IsRequired();

            builder
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy);

            builder
                .HasOne(p => p.Motorcycle)
                .WithMany()
                .HasForeignKey(p => p.MotorcycleId);

        }
    }
}
