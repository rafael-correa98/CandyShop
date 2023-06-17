using CandyShopApi.Models;
using Microsoft.Extensions.Localization;

namespace CandyShopApi.BusinessRules.Interface
{
    public interface ICategoriesBusiness
    {
        Task<List<Category>> GetAllCategoriesAsync();

        Task<Category?> GetCategoryByIdAsync(string id);

        Task CreateCategoryAsync(Category newCategory);

        Task UpdateCategoryAsync(string id, Category updatedCategoryy);

        Task RemoveCategoryAsync(string id);
    }
}
