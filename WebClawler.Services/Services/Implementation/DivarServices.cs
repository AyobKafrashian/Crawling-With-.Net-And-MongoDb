using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCrawler.DataLayer.Model;
using WebCrawler.DataLayer.MongoSetting;

namespace WebClawler.Services.Services.Implementation
{
    public class DivarServices
    {
        private readonly IMongoCollection<Divar> _DivarCollection;

        public DivarServices(
            IOptions<DivarMongoSetting> DivarDataBaseSetting)
        {
            var mongoClient = new MongoClient(
                DivarDataBaseSetting.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                DivarDataBaseSetting.Value.DatabaseName);

            _DivarCollection = mongoDatabase.GetCollection<Divar>(
                    DivarDataBaseSetting.Value.DivarsCollectionName);
        }

        public async Task<List<Divar>> GetAsync() =>
            await _DivarCollection.Find(_ => true).SortByDescending(c => c.CreateDate).ToListAsync();

        public async Task<Divar?> GetAsync(string id) =>
            await _DivarCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Divar newDivar) =>
            await _DivarCollection.InsertOneAsync(newDivar);

        public async Task UpdateAsync(string id, Divar updatedDivar) =>
            await _DivarCollection.ReplaceOneAsync(x => x.Id == id, updatedDivar);

        public async Task RemoveAsync(string id) =>
            await _DivarCollection.DeleteOneAsync(x => x.Id == id);
    }
}
