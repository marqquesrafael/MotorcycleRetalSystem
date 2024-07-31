using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRentalSystem.Domain.Entities.Motorcycle;
using MotorcycleRentalSystem.Infrastructure.Mapping;

namespace MotorcycleRentalSystem.Infrastructure.Persistence.Mapping.Motorcycle
{
    public class MotorcycleMap : BaseMap<MotorcycleEntity>
    {
        public override string GetTableName() => "tb_motorcycle";

        public override void Configure(EntityTypeBuilder<MotorcycleEntity> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Year)
                .HasColumnName("year")
                .IsRequired();

            builder.Property(p => p.Model)
                .HasColumnName("model")
                .IsRequired();

            builder.Property(p => p.LicensePlate)
                .HasColumnName("license_plate")
                .HasConversion(p => p.ToUpper(), p => p.ToUpper())
                .IsRequired();

            builder.HasIndex(p => p.LicensePlate)
                .IsUnique();

        }
    }
}
