using Rent.Application.ViewModels;

namespace Rent.Application.Interfaces
{
    public interface IDriverAppService
    {
        Task<MessageViewModel> CreateAsync(CreateDriverViewModel newDriver);
    }
}
