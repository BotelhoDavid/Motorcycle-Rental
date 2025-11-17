using Rent.Domain.Models;

namespace Rent.Domain.Entities
{
    public class Rent : Entity
    {
        public Guid Driver_id { get; set; }
        public Guid Moto_id { get; set; }
        public DateTime Initio_date { get; set; }
        public DateTime End_date { get; set; }
        public DateTime Forecast_end_date { get; set; }
        public DateTime Return_date { get; set; }
        public int Plan { get; set; }

        public virtual Moto Moto { get; set; }
        public virtual Driver Driver { get; set; }

        public void SetReturnDate(DateTime return_date)
        {
            Return_date = return_date;
        }
    }
}
