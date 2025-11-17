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
            builder.HasKey(rent => rent.Id);

            builder.Property(rent => rent.Driver_id).IsRequired();
            builder.Property(rent => rent.Moto_id).IsRequired();
            builder.Property(rent => rent.Initio_date).IsRequired();
            builder.Property(rent => rent.Forecast_end_date).IsRequired();

            builder.HasOne(rent => rent.Moto)
               .WithMany(moto => moto.Rents)
               .HasForeignKey(rent => rent.Moto_id)
               .OnDelete(DeleteBehavior.Restrict);            

            builder.HasOne(rent => rent.Driver)
                   .WithMany(driver => driver.Rents)
                   .HasForeignKey(rent => rent.Driver_id)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}