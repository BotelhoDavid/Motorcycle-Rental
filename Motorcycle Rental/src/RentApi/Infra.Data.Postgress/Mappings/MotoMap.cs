using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rent.Domain.Entities;

namespace Rent.Infra.Data.Postgress.Mappings
{
    public class MotoMap : IEntityTypeConfiguration<Moto>
    {
        public void Configure(EntityTypeBuilder<Moto> builder)
        {
            builder.ToTable("Motos");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Plate).IsRequired().HasMaxLength(10);
            builder.Property(p => p.Model).IsRequired().HasMaxLength(25);

            builder.HasMany(m => m.Rents)
               .WithOne(r => r.Moto)
               .HasForeignKey(r => r.Moto_id)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.Plate)
                   .IsUnique();
        }
    }
}
