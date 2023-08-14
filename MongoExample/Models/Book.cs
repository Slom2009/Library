using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoExample.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? AuthorId { get; set; }

    }
}
