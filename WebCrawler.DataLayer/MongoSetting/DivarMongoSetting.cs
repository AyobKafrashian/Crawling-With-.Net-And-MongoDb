namespace WebCrawler.DataLayer.MongoSetting
{
    public class DivarMongoSetting
    {
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";

        public string DatabaseName { get; set; } = "DivarCrawlerDB";

        public string DivarsCollectionName { get; set; } = "Divars";
    }
}
