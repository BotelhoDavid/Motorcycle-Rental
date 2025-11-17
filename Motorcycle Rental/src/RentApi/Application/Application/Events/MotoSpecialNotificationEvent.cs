namespace Rent.Application.Events
{
    public class MotoSpecialNotificationEvent
    {
        public MotoSpecialNotificationEvent() { Message = "Moto cadastrada com placa 2024"; }
        public Guid Moto_id { get; set; }
        public string Plate { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
