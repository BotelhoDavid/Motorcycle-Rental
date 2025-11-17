using Mapster;
using Rent.Application.Events;
using Rent.Application.ViewModels;
using Rent.Domain.Entities;
using EntityRent = Rent.Domain.Entities.Rent;

namespace Rent.Application.Mapping
{
    public class MapsterConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.Default.PreserveReference(true);
            config.Default.IgnoreNullValues(true);

            config.NewConfig<Moto, MotoReturnViewModel>();

            config.NewConfig<Moto, MotoCreatedEvent>();
            config.NewConfig<Moto, MotoSpecialNotificationEvent>();

            config.NewConfig<CreateMotoViewModel, Moto>();

            config.NewConfig<EntityRent, RentalViewModel>();
            config.NewConfig<CreateRentalViewModel, EntityRent>();


        }
    }
}
