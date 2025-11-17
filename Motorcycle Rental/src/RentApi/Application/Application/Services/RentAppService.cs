using MapsterMapper;
using Rent.Application.Interfaces;
using Rent.Application.ViewModels;
using Rent.Domain.Interfaces.Repositories.Postgres;
using Rent.Domain.Models;
using EntityRent = Rent.Domain.Entities.Rent;

namespace Rent.Application.Services
{
    public class RentAppService : IRentAppService
    {
        private readonly IRentRepository _repository;
        private readonly IMapper _mapper;
        public RentAppService(IRentRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<MessageViewModel> CreateAsync(CreateRentalViewModel newRental)
        {
            MessageViewModel message = new MessageViewModel();
            EntityRent rental = _mapper.Map<EntityRent>(newRental);

            await _repository.CreateAsync(rental);

            bool succesfully = await _repository.CommitAsync();

            if (!succesfully)
            {
                message.SetMessage("Invalid data");
            }

            message.SetSuccess(succesfully);

            return message;
        }

        public async Task<RentalViewModel> GetAsync(Guid id)
        {
            RentalViewModel rent = new RentalViewModel();
            EntityRent? _rent = await _repository.GetAsync(id);

            if (_rent is null)
            {
                rent.SetSuccess(false);
            }
            else
                _mapper.Map(_rent, rent);

            decimal _value = RentalPlan.FromDays(_rent.Plan).DailyCost;

            if (_rent.Return_date != default && _rent.Return_date != _rent.Forecast_end_date)
            {
                TimeSpan difference = _rent.Return_date - _rent.Forecast_end_date;
                int extraDays = (int)Math.Ceiling(difference.TotalDays);
                if (extraDays > 0)
                {
                    _value += extraDays * 50;
                }
                else
                {
                    int days = (int)(_rent.Forecast_end_date.Date - _rent.Return_date.Date).TotalDays;
                    decimal fee = default;

                    switch (_rent.Plan)
                    {
                        case 7:
                            fee = (_value * days / 100) * 20;
                            break;
                        case 15:
                            fee = (_value * days / 100) * 40;
                            break;
                        default:
                            break;
                    }
                    _value += fee;
                }
            }

            rent.SetDailyValue(_value);

            return rent;
        }

        public async Task<MessageViewModel> SetReturnAsync(Guid id, RentalReturnViewModel rent)
        {
            try
            {
                MessageViewModel message = new MessageViewModel();

                EntityRent _rent = await _repository.GetAsync(id) ?? throw new Exception();

                _rent.SetReturnDate(rent.Return_date);

                await _repository.UpdateAsync(_rent);

                bool succesfully = await _repository.CommitAsync();

                if (!succesfully)
                    throw new Exception();

                message.SetSuccess(succesfully);
                message.SetMessage("Return date successfully provided");

                return message;
            }
            catch (Exception)
            {
                MessageViewModel message = new MessageViewModel();

                message.SetMessage("Invalid data");
                message.SetSuccess(false);

                return message;
            }
        }
    }
}
