using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CandyShopApi.Models
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("customer_name")]
        public string CustomerName { get; set; } = null!;

        [BsonElement("product_name")]
        public string ProductName { get; set; } = null!;


        public Int32 Rating { get; set; }
    }
}
