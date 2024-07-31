using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRentalSystem.Domain.Entities.User;
using MotorcycleRentalSystem.Domain.Enums.User;

namespace MotorcycleRentalSystem.Infrastructure.Mapping.User
{
    public class UserMap : BaseMap<UserEntity>
    {
        public override string GetTableName() => "tb_user";

        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.FullName)
                .HasColumnName("full_name")
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnName("email")
                .IsRequired();

            builder.Property(p => p.Password)
                .HasColumnName("password")
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .HasColumnName("phone_number")
                .IsRequired();

            builder.Property(p => p.Type)
                .HasColumnName("type")
                .HasConversion<string>()
                .IsRequired();

            builder.HasData(
                new UserEntity("system@motorcyclerentalsystem.com", "1234", "System", "31 99999-9999", UserTypeEnum.System) { Id = 1, CreatedAt = DateTime.UtcNow },
                new UserEntity("admin@motorcyclerentalsystem.com", "1234", "Administrator", "31 99999-9999", UserTypeEnum.Administrator) { Id = 2, CreatedAt = DateTime.UtcNow }
                );
        }
    }
}
