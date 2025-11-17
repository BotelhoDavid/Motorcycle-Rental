using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LogAPI.Entities
{
    public class LogEntry
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [BsonRepresentation(BsonType.String)]
        public Guid Moto_id { get; set; }
        public string Plate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow; public string Message { get; set; }
        public void InsertMessage(string message) { Message = message; }
    }
}
