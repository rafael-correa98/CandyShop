using CandyShopApi.Models;
using CandyShopApi.Services.Interface;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CandyShopApi.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IMongoCollection<Category> _categoriesCollection;

        public CategoriesService(
            IOptions<CandyShopDatabaseSettings> candyStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                candyStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                candyStoreDatabaseSettings.Value.DatabaseName);

            _categoriesCollection = mongoDatabase.GetCollection<Category>(
                candyStoreDatabaseSettings.Value.CategoryCollectionName);
        }
        public Task<List<Category>> GetAsync() =>
            _categoriesCollection.Find(_ => true).ToListAsync();

        public async Task<Category?> GetAsync(string id) =>
            await _categoriesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Category newCategory) =>
            await _categoriesCollection.InsertOneAsync(newCategory);

        public async Task UpdateAsync(string id, Category updatedCategory) =>
            await _categoriesCollection.ReplaceOneAsync(x => x.Id == id, updatedCategory);

        public async Task RemoveAsync(string id) =>
            await _categoriesCollection.DeleteOneAsync(x => x.Id == id);
    }
}
