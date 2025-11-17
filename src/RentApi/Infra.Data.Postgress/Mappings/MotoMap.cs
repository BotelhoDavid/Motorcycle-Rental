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
            builder.HasKey(moto => moto.Id);

            builder.Property(moto => moto.Plate).IsRequired().HasMaxLength(10);
            builder.Property(p => p.Model).IsRequired().HasMaxLength(25);

            builder.HasMany(moto => moto.Rents)
               .WithOne(rent => rent.Moto)
               .HasForeignKey(moto => moto.Moto_id)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(moto => moto.Plate)
                   .IsUnique();
        }
    }
}
