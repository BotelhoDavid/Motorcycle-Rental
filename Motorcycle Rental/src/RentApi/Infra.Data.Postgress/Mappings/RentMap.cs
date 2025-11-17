using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentEntity = Rent.Domain.Entities.Rent;

namespace Rent.Infra.Data.Postgress.Mappings
{
    public class RentMap : IEntityTypeConfiguration<RentEntity>
    {
        public void Configure(EntityTypeBuilder<RentEntity> builder)
        {
            builder.ToTable("Rents");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Driver_id).IsRequired();
            builder.Property(p => p.Moto_id).IsRequired();
            builder.Property(p => p.Initio_date).IsRequired();
            builder.Property(p => p.Forecast_end_date).IsRequired();

            builder.HasOne(r => r.Moto)
               .WithMany(m => m.Rents)
               .HasForeignKey(r => r.Moto_id)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Driver)
                   .WithMany(d => d.Rents)
                   .HasForeignKey(r => r.Driver_id)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}