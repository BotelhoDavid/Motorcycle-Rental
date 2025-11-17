using System;
using System.Collections.Generic;
using System.Text;

namespace Rent.Application.ViewModels
{
    public class CreateMotoViewModel
    {
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }

        public void Normalize()
        {
            Model = Model?.ToUpper();
            Plate = Plate?.Trim().ToUpper();
        }
    }
}
