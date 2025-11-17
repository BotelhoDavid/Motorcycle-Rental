using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.Application.ViewModels
{
    public class RentalViewModel
    {
        public Guid Id { get; set; }
        public Guid Driver_id { get; set; }
        public Guid Moto_id { get; set; }
        public DateTime Initio_date { get; set; }
        public DateTime End_date { get; set; }
        public DateTime Forecast_end_date { get; set; }
        public DateTime Devolution_date { get; set; }
        public decimal Daily_value { get; set; }
    }
}
