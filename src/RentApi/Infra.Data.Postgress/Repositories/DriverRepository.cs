using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories.Postgres;
using Rent.Infra.Data.Postgress.Context;

namespace Rent.Infra.Data.Postgress.Repositories
{
    public class DriverRepository : Repository<Driver>, IDriverRepository
    {
        public DriverRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
