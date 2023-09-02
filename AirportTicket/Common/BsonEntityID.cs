using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AirportTicket.Common;

public class BsonEntityID
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
}
