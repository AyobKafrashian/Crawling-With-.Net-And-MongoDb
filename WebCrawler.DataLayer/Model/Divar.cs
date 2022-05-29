using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace WebCrawler.DataLayer.Model
{
    public class Divar
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Title { get; set; }
        public string Address { get; set; }
        public string Price { get; set; }
        public string? ImageUrl { get; set; }
        public string Link { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
