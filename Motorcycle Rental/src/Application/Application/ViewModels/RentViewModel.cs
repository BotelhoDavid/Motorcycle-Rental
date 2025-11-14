namespace Application
{
    public class RentViewModel
    {
        public Guid Id { get; set; }
        public int Daily_Value { get; set; }
        public Guid Driver_Id { get; set; }
        public Guid Moto_id { get; set; }
        public DateTime Initio_date { get; set; }
        public DateTime End_date { get; set; }
        public DateTime Forecast_end_date { get; set; }
        public DateTime Devolution_date { get; set; }

    }
}
