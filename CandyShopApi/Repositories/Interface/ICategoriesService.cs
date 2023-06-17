using CandyShopApi.Models;

namespace CandyShopApi.Services.Interface
{
    public interface ICategoriesService
    {
        Task<List<Category>> GetAsync();

        Task<Category?> GetAsync(string id);

        Task CreateAsync(Category newCategory);

        Task UpdateAsync(string id, Category updatedCategory);

        Task RemoveAsync(string id);
    }
}
