using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.Domain.Events
{
    public record MotoCreatedEvent(Guid Id, string Modelo, string Placa) : IIntegrationEvent;
}
