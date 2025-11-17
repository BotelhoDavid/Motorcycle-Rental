using Rent.Domain.Interfaces.Repositories.Postgres;
using Rent.Infra.Data.Postgress.Context;
using RentEntity = Rent.Domain.Entities.Rent;

namespace Rent.Infra.Data.Postgress.Repositories
{
    public class RentRepository : Repository<RentEntity>, IRentRepository
    {
        public RentRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}