using System.Text.Json.Serialization;

namespace Rent.Application.ViewModels
{
    public class MessageViewModel
    {
        public MessageViewModel() { }
        public MessageViewModel(string message) { Message = message; }

        public string Message { get; set; }

        [JsonIgnore]
        public bool Success { get; set; }

        public void SetMessage(string message)
        {
            Message = message;
        }

        public void SetSuccess(bool success)
        {
            Success = success;
        }
    }
}
