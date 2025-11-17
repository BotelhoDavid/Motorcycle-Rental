namespace Rent.Application.ViewModels
{
    public class CreateRentalViewModel
    {
        public Guid Driver_id { get; set; }
        public Guid Moto_id { get; set; }
        public DateTime Initio_date { get; set; }
        public DateTime End_date { get; set; }
        public DateTime Forecast_end_date { get; set; }
        public int Plan { get; set; }
    }
}
