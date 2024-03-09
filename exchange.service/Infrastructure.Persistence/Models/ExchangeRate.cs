using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Persistence.Models
{
    public class ExchangeRate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public required string FromCurrencyCode { get; set; }

        public required string ToCurrencyCode { get; set; }

        public double BidPrice { get; set; }

        public double AskPrice { get; set; }
    }
}
