using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Rent.Application.Events;
using Rent.Application.Interfaces;
using Rent.Application.ViewModels;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.MessageBus;
using Rent.Domain.Interfaces.Repositories.Postgres;

namespace Rent.Application.Services
{
    public class MotoAppService : IMotoAppService
    {
        private readonly IMotoRepository _repositoryMoto;
        private readonly IMapper _mapper;
        private readonly IRabbitMqProducer _rabbitMqProducer;

        public MotoAppService(IMotoRepository repositoryMoto,
                              IMapper mapper,
                              IRabbitMqProducer rabbitMqProducer)
        {
            _repositoryMoto = repositoryMoto;
            _mapper = mapper;
            _rabbitMqProducer = rabbitMqProducer;
        }

        public async Task<MessageViewModel> CreateAsync(CreateMotoViewModel newMoto)
        {
            MessageViewModel message = new MessageViewModel();
            newMoto.Normalize();
            Moto moto = _mapper.Map<Moto>(newMoto);

            await _repositoryMoto.CreateAsync(moto);
            bool successfully = await _repositoryMoto.CommitAsync();

            if (!successfully)
            {
                message.SetMessage("Invalid data");
                message.SetSuccess(false);
                return message;
            }

            message.SetSuccess(true);

            _rabbitMqProducer.PublishAsync(_mapper.Map<MotoCreatedEvent>(moto), "motos.created");

            if (moto.Year == 2024)
            {
                var teste = _mapper.Map<MotoSpecialNotificationEvent>(moto);
                _rabbitMqProducer.PublishAsync(teste, "motos.notification");
            }

            return message;
        }

        public async Task<MessageViewModel> DeleteAsync(Guid id)
        {
            await _repositoryMoto.DeleteAsync(id);

            MessageViewModel message = new MessageViewModel();

            bool succesfully = await _repositoryMoto.CommitAsync();

            if (!succesfully)
                message.SetMessage("Invalid data");
            else
                message.SetMessage("Moto successfully deleted!");

            message.SetSuccess(succesfully);

            return message;
        }

        public async Task<MotoReturnViewModel> GetAsync(Guid id)
        {
            MotoReturnViewModel moto = new MotoReturnViewModel();
            Moto? _moto = await _repositoryMoto.GetAsync(id);

            if (_moto is null)
            {
                moto.SetSuccess(false);
            }
            else
                _mapper.Map(_moto, moto);

            return moto;
        }

        public async Task<List<MotoReturnViewModel>> GetAllAsync()
        {
            List<Moto> _motos = await _repositoryMoto.Query(moto => !moto.IsDeleted).ToListAsync();

            return _mapper.Map<List<MotoReturnViewModel>>(_motos);
        }

        public async Task<MessageViewModel> UpdateAsync(Guid id, UpdateMotoViewModel moto)
        {
            MessageViewModel message = new MessageViewModel();
            try
            {
                Moto motoOld = await _repositoryMoto.GetAsync(id) ?? throw new Exception();

                motoOld.UpdatePlate(moto.Plate);

                await _repositoryMoto.UpdateAsync(motoOld);

                bool succesfully = await _repositoryMoto.CommitAsync();

                if (!succesfully)
                    throw new Exception();

                message.SetSuccess(succesfully);
                message.SetMessage("Plate modified successfully");
            }
            catch (Exception)
            {
                message.SetMessage("Invalid data");
            }
            return message;
        }
    }
}
