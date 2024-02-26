using BLL.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BLL.Entities
{
    public class Module
    {
        [BsonElement("Order")]
        public int Order { get; set; }

        [BsonElement("Type")]
        public ModuleType Type { get; set; }

        [BsonElement("Content")]
        public string Content { get; set; }
    }
}
