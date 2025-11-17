using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.Application.Events
{
    public class MotoCreatedEvent
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public int Year { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
