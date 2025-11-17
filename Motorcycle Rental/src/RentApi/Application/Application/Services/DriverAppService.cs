using MapsterMapper;
using Rent.Application.Interfaces;
using Rent.Application.ViewModels;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories.Postgres;

namespace Rent.Application.Services
{
    public class DriverAppService : IDriverAppService
    {
        private readonly IDriverRepository _repositoryDriver;
        private readonly IMapper _mapper;

        public DriverAppService(IDriverRepository repositoryDriver,
                                IMapper mapper)
        {
            _repositoryDriver = repositoryDriver;
            _mapper = mapper;
        }
        public async Task<MessageViewModel> CreateAsync(CreateDriverViewModel newDriver)
        {
            MessageViewModel message = new MessageViewModel();
            Driver driver = _mapper.Map<Driver>(newDriver);

            await _repositoryDriver.CreateAsync(driver);

            bool succesfully = await _repositoryDriver.CommitAsync();

            if (!succesfully)
            {
                message.SetMessage("Invalid data");
            }

            message.SetSuccess(succesfully);

            return message;
        }
    }
}
