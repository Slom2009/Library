using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoExample.Models
{
    public class Author
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }

    }
}
