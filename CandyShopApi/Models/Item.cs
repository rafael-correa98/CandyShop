using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace CandyShopApi.Models
{
    public class Item
    {
        [BsonElement("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;


        [BsonElement("quantity")]
        [JsonPropertyName("quantity")]
        public Int32 Quantity { get; set; }


        [BsonElement("price")]
        [JsonPropertyName("price")]
        public double Price { get; set; }
    }
}
