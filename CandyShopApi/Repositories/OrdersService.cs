using CandyShopApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CandyShopApi.Services
{
    public class OrdersService
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public OrdersService(
            IOptions<CandyShopDatabaseSettings> candyStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                candyStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                candyStoreDatabaseSettings.Value.DatabaseName);

            _orderCollection = mongoDatabase.GetCollection<Order>(
                candyStoreDatabaseSettings.Value.OrdersCollectionName);
        }

        public async Task<List<Order>> GetAsync() =>
            await _orderCollection.Find(_ => true).ToListAsync();

        public async Task<Order?> GetAsync(string id) =>
           await _orderCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Order newOrder) =>
            await _orderCollection.InsertOneAsync(newOrder);

        public async Task UpdateAsync(string id, Order updatedOrder) =>
            await _orderCollection.ReplaceOneAsync(x => x.Id == id, updatedOrder);

        public async Task RemoveAsync(string id) =>
            await _orderCollection.DeleteOneAsync(x => x.Id == id);
    }
}
