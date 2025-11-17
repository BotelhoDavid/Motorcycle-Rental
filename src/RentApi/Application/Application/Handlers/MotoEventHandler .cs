using MapsterMapper;
using Rent.Application.Events;
using Rent.Application.Interfaces;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories.Mongo;

namespace Rent.Application.Handlers
{
    public class MotoEventHandler : IMotoEventHandler
    {
        private readonly ILogRepository _logRepository;
        private readonly IMapper _mapper;

        public MotoEventHandler(ILogRepository logRepository,
                                IMapper mapper)
        {
            _logRepository = logRepository;
            _mapper = mapper;
        }

        public async Task HandleAsync(MotoSpecialNotificationEvent notification)
        {
            Log log = _mapper.Map<Log>(notification);

            log.InsertMessage( $"Moto com placa {notification.Plate} gerou notificação especial.");

            _logRepository.InsertOne(log);
        }
    }
}

