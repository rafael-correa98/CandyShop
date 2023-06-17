using CandyShopApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CandyShopApi.Services
{
    public class CustomersService
    {
        private readonly IMongoCollection<Customer> _customerCollection;

        public CustomersService(
            IOptions<CandyShopDatabaseSettings> candyStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                candyStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                candyStoreDatabaseSettings.Value.DatabaseName);

            _customerCollection = mongoDatabase.GetCollection<Customer>(
                candyStoreDatabaseSettings.Value.CustomersCollectionName);
        }

        public async Task<List<Customer>> GetAsync() =>
            await _customerCollection.Find(_ => true).ToListAsync();

        public async Task<Customer?> GetAsync(string id) =>
           await _customerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Customer newCustomer) =>
            await _customerCollection.InsertOneAsync(newCustomer);

        public async Task UpdateAsync(string id, Customer updatedCustomer) =>
            await _customerCollection.ReplaceOneAsync(x => x.Id == id, updatedCustomer);

        public async Task RemoveAsync(string id) =>
            await _customerCollection.DeleteOneAsync(x => x.Id == id);
    }
}
