using Rent.Domain.Models;

namespace Rent.Domain.Entities
{
    public class Moto : Entity
    {
        public int Year { get; set; }
        public required string Model { get; set; }
        public required string Plate { get; set; }

        public virtual ICollection<Rent> Rents { get; set; }

        public void UpdatePlate(string plate)
        {
            Plate = plate;
        }
    }
}
