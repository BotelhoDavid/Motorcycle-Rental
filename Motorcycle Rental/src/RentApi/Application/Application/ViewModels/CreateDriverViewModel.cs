namespace Rent.Application.ViewModels
{
    public class CreateDriverViewModel
    {
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime Birth { get; set; }
        public string CNHNumber { get; set; }
        public string CNHtype { get; set; }
        public Guid CNHImage { get; set; }
    }
}
