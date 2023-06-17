using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace CandyShopApi.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("customer_name")]
        [JsonPropertyName("customer_name")]
        public string CustomerName { get; set; } = null!;

        [BsonElement("delivery_address")]
        [JsonPropertyName("delivery_address")]
        public string DeliveryAddress { get; set; } = null!;

        [BsonElement("items")]
        [JsonPropertyName("items")]
        public List<Item>Items { get; set; } = null!;

        [BsonElement("total")]
        [JsonPropertyName("total")]
        public Int32 Total { get; set; }


        [BsonElement("status")]
        [JsonPropertyName("status")]
        public string Status { get; set; } = null!;
    }
}



