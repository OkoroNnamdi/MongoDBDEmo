using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbDemo.Model
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
      public   string Id { get; set; }
      public string? ProductName { get; set; }
        public string CategoryId { get; set; }
    }
}
