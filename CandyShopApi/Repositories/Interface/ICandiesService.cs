using CandyShopApi.Models;

namespace CandyShopApi.Services.Interface
{
    public interface ICandiesService
    {
        Task<List<Candy>> GetAsync();

        Task<Candy?> GetAsync(string id);

        Task CreateAsync(Candy newCandy);

        Task UpdateAsync(string id, Candy updatedCandy);

        Task RemoveAsync(string id);
    }
}
