using Rent.Application.ViewModels;

namespace Rent.Application.Interfaces
{
    public interface IRentAppService
    {
        Task<MessageViewModel> CreateAsync(CreateRentalViewModel newRental);
        Task<RentalViewModel> GetAsync(Guid id);
        Task<MessageViewModel> SetReturnAsync(Guid id, RentalReturnViewModel moto);
    }
}
