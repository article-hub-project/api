using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BLL.Entities
{
    public class Article
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("AuthorId")]
        public string AuthorId { get; set; }

        [BsonElement("PublishedDate")]
        public DateTime PublishedDate { get; set; }

        [BsonElement("Modules")]
        public List<Module> Modules { get; set; }
    }
}
