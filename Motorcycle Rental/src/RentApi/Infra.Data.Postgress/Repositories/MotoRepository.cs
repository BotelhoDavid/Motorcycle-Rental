using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories.Postgres;
using Rent.Infra.Data.Postgress.Context;

namespace Rent.Infra.Data.Postgress.Repositories
{
    public class MotoRepository : Repository<Moto>, IMotoRepository
    {
        public MotoRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
