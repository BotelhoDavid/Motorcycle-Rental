namespace Rent.Domain.Entities
{
    public class Driver
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime Birth { get; set; }
        public string CNHNumber { get; set; }
        public string CNHtype { get; set; }
        public Guid CNHImage { get; set; }

        public virtual ICollection<Rent> Rents { get; set; }
    }
}
