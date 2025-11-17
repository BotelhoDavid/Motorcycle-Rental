using Rent.Application.ViewModels;

namespace Rent.Application.Interfaces
{
    public interface IMotoAppService
    {
        Task<MessageViewModel> CreateAsync(CreateMotoViewModel newMoto);
        Task<List<MotoReturnViewModel>> GetAllAsync();
        Task<MotoReturnViewModel> GetAsync(Guid id);
        Task<MessageViewModel> UpdateAsync(Guid id, UpdateMotoViewModel moto);
        Task<MessageViewModel> DeleteAsync(Guid id);
    }
}
