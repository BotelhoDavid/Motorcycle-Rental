using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rent.Domain.Entities;

namespace Rent.Infra.Data.Postgress.Mappings
{
    public class DriverMap : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("Drivers");
            builder.HasKey(driver => driver.Id);

            builder.Property(driver => driver.Name).HasMaxLength(50);
            builder.Property(driver => driver.CNPJ).HasMaxLength(25);
            builder.Property(driver => driver.CNHNumber).HasMaxLength(15);
            builder.Property(driver => driver.CNHtype).HasMaxLength(3);

            builder.HasMany(driver => driver.Rents)
               .WithOne(rent => rent.Driver)
               .HasForeignKey(driver => driver.Driver_id)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(driver => driver.CNPJ)
                   .IsUnique();

            builder.HasIndex(driver => driver.CNHNumber)
                   .IsUnique();
        }
    }
}
