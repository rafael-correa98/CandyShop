using CandyShopApi.Models;
using CandyShopApi.Services.Interface;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CandyShopApi.Services
{
    public class CandiesService : ICandiesService
    {
        private readonly IMongoCollection<Candy> _candiesCollection;

        public CandiesService(
            IOptions<CandyShopDatabaseSettings> candyStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                candyStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                candyStoreDatabaseSettings.Value.DatabaseName);

            _candiesCollection = mongoDatabase.GetCollection<Candy>(
                candyStoreDatabaseSettings.Value.CandiesCollectionName);
        }
        public Task<List<Candy>> GetAsync() =>
            _candiesCollection.Find(_ => true).ToListAsync();

        public async Task<Candy?> GetAsync(string id) =>
            await _candiesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Candy newCandy) =>
            await _candiesCollection.InsertOneAsync(newCandy);

        public async Task UpdateAsync(string id, Candy updatedCandy) =>
            await _candiesCollection.ReplaceOneAsync(x => x.Id == id, updatedCandy);

        public async Task RemoveAsync(string id) =>
            await _candiesCollection.DeleteOneAsync(x => x.Id == id);
    }
}
