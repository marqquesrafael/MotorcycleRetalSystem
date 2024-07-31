using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRentalSystem.Domain.Entities.Rider;
using MotorcycleRentalSystem.Infrastructure.Mapping;

namespace MotorcycleRentalSystem.Infrastructure.Persistence.Mapping.Rider
{
    public class RiderMap : BaseMap<RiderEntity>
    {
        public override string GetTableName() => "tb_rider";

        public override void Configure(EntityTypeBuilder<RiderEntity> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.Cnpj)
                .IsRequired()
                .HasMaxLength(14);

            builder.HasIndex(e => e.Cnpj)
                .IsUnique();

            builder.Property(e => e.BirthDate)
                .IsRequired();

            builder.Property(e => e.DriverLicenseNumber)
                .IsRequired();

            builder.HasIndex(e => e.DriverLicenseNumber)
                .IsUnique();

            builder.Property(e => e.DriverLicenseType)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(e => e.DriverLicenseImageKey)
                .IsRequired();

        }
    }
}
