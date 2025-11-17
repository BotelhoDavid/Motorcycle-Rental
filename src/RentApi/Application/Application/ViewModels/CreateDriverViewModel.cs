using System.Runtime.Intrinsics.Arm;

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


        public bool IsCNHValid()
         => CNHtype.Equals("A") || CNHtype.Equals("B") || CNHtype.Equals("A+B");

        public void Normalize()
        {
            CNPJ = CNPJ?.Trim().ToUpper();
            CNHtype = CNHtype?.Trim().ToUpper();
        }
    }
}
