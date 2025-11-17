using RentEntity = Rent.Domain.Entities.Rent;

namespace Rent.Domain.Interfaces.Repositories.Postgres
{
    public interface IRentRepository : IRepository<RentEntity>
    {
    }
}
