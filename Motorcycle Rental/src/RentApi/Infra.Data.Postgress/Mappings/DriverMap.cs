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
            builder.HasKey(p => p.Id);            
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.CNPJ).HasMaxLength(25);
            builder.Property(p => p.CNHNumber).HasMaxLength(15);
            builder.Property(p => p.CNHtype).HasMaxLength(3);

            builder.HasMany(m => m.Rents)
               .WithOne(r => r.Driver)
               .HasForeignKey(r => r.Driver_id)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.CNPJ)
                   .IsUnique();

            builder.HasIndex(p => p.CNHNumber)
                   .IsUnique();
        }
    }
}
