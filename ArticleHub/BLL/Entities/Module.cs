using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BLL.Entities
{
    public class Module
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Order")]
        public int Order { get; set; }

        // todo: create enum for module type
        [BsonElement("Type")]
        public string Type { get; set; }

        [BsonElement("Content")]
        public string Content { get; set; }
    }
}
