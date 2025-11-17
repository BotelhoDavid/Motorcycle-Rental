using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.Domain.Entities
{
    public class Log
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public Guid Moto_Id { get; set; }
        public string Plate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string Message { get; set; }

        public void InsertMessage(string message) { Message = message; }
    }
}
