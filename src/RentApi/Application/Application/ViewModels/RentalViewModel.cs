using System.Text.Json.Serialization;

namespace Rent.Application.ViewModels
{
    public class RentalViewModel
    {
        public RentalViewModel() { Success = true; }

        public Guid Id { get; set; }
        public Guid Driver_id { get; set; }
        public Guid Moto_id { get; set; }
        public DateTime Initio_date { get; set; }
        public DateTime End_date { get; set; }
        public DateTime Forecast_end_date { get; set; }
        public DateTime Return_date { get; set; }
        public decimal Daily_value { get; set; }

        [JsonIgnore]
        public bool Success { get; set; }

        internal void SetSuccess(bool success)
        {
            Success = success;
        }

        internal void SetDailyValue(decimal value)
        {
            Daily_value = value;
        }
    }
}
