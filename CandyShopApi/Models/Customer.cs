using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace CandyShopApi.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string? Id { get; set; }

        [BsonElement("delivery_address")]
        [JsonPropertyName("delivery_address")]
        public string DeliveryAddress { get; set; } = null!;

        [BsonElement("email")]
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [BsonElement("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;


        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("order_history")]
        [JsonPropertyName("order_history")]
        public List<string>? OrderHistory { get; set; } = null!;


        [BsonElement("phone")]
        [JsonPropertyName("phone")]
        public string Phone { get; set; } = null!;
    }
}
