using CandyShopApi.Models;
using Microsoft.Extensions.Localization;

namespace CandyShopApi.BusinessRules.Interface
{
    public interface ICandiesBusiness
    {
        Task<List<Candy>> GetAllCandiesAsync();

        Task<Candy?> GetCandyByIdAsync(string id);

        Task CreateCandyAsync(Candy newCandy);

        Task UpdateCandyAsync(string id, Candy updatedCandy);

        Task RemoveCandyAsync(string id);
    }
}
