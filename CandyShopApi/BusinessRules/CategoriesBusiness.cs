using CandyShopApi.BusinessRules;
using CandyShopApi.BusinessRules.Interface;
using CandyShopApi.Errors;
using CandyShopApi.Models;
using CandyShopApi.Services;
using CandyShopApi.Services.Interface;

namespace CandyShopApi.BusinessRules
{
    public class CategoriesBusiness: ICategoriesBusiness
    {
        private readonly CategoriesService _categoriesService;

        public CategoriesBusiness(CategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categoriesService.GetAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(string id)
        {
            var categoryFound =  await _categoriesService.GetAsync(id);

            if (categoryFound is null)
            {
                throw new HttpException("Categoria não encontrada", 404);
            }

            return categoryFound;
        }

        public async Task CreateCategoryAsync(Category newCategory)
        {
            var categoryAlreadyExists = await VerifyCategoryNameAlreadyExist(newCategory);
            if (categoryAlreadyExists != null)
            {
                throw categoryAlreadyExists;
            }

            await _categoriesService.CreateAsync(newCategory);
        }

        public async Task UpdateCategoryAsync(string id, Category updatedCategory)
        {
            var categoryAlreadyExists = await VerifyCategoryNameAlreadyExist(updatedCategory);
            if (categoryAlreadyExists != null)
            {
                throw categoryAlreadyExists;
            }

            var categoryFound = await _categoriesService.GetAsync(id);

            if (categoryFound is null)
            {
                throw new HttpException("Categoria não encontrada", 404);
            }

            updatedCategory.Id = categoryFound.Id;

            await _categoriesService.UpdateAsync(id, updatedCategory);
        }

        public async Task RemoveCategoryAsync(string id)
        {
            var categoryFound = _categoriesService.GetAsync(id);

            if (categoryFound is null)
            {
                throw new HttpException("Categoria não encontrada", 404);
            }

            await _categoriesService.RemoveAsync(id);
        }

        private async Task<HttpException?> VerifyCategoryNameAlreadyExist(Category categoryForVerify)
        {
            var allCategories = await _categoriesService.GetAsync();

            if (allCategories.Count > 0)
            {
                bool nameAlreadyExist = allCategories.Exists(category => category.Name == categoryForVerify.Name);
                if (nameAlreadyExist)
                {
                    return new HttpException("Categoria já existe", 400);
                }
            }

            return null;
        }
    }
}