namespace Domain.Entities
{
    public class Rent
    {
        public Guid Driver_id { get; set; }
        public Guid Moto_id { get; set; }
        public DateTime date_initio { get; set; }
        public DateTime date_end { get; set; }
        public DateTime date_forecast_end { get; set; }
        public int plan { get; set; }
    }
}
