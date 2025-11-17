using System.Text.Json.Serialization;

namespace Rent.Application.ViewModels
{

    public class MotoReturnViewModel
    {
        public MotoReturnViewModel() { Success = true; }
        public Guid Id { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }

        [JsonIgnore]
        public bool Success { get; set; }

        internal void SetSuccess(bool success)
        {
            Success = success;
        }
    }
}
