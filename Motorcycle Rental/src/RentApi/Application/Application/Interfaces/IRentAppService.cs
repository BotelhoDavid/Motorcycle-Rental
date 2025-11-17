using Rent.Application.ViewModels;

namespace Rent.Application.Interfaces
{
    public interface IRentAppService
    {
        Task<RentalViewModel> CreateAsync(CreateRentalInputModel model);
        Task<RentalViewModel> GetByIdAsync(Guid id);
        Task<RentalViewModel> SetDevolutionAsync(Guid id, RentalDevolutionInputModel model);
    }
}
