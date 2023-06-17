using CandyShopApi.BusinessRules.Interface;
using CandyShopApi.Errors;
using CandyShopApi.Models;
using CandyShopApi.Services;
using CandyShopApi.Services.Interface;

namespace CandyShopApi.BusinessRules
{
    public class CandiesBusiness: ICandiesBusiness
    {
        private readonly CandiesService _candiesService;

        public CandiesBusiness(CandiesService candiesService)
        {
            _candiesService = candiesService;
        }
        public async Task<List<Candy>> GetAllCandiesAsync()
        {
            return await _candiesService.GetAsync();
        }

        public async Task<Candy?> GetCandyByIdAsync(string id)
        {
            var candyFound = await _candiesService.GetAsync(id);

            if (candyFound is null)
            {
                throw new HttpException("Doce não encontrado", 404);
            }

            return candyFound;
        }

        public async Task CreateCandyAsync(Candy newCandy)
        {
            var candyAlreadyExists = await VerifyCandyNameAlreadyExist(newCandy);
            if (candyAlreadyExists != null)
            {
                throw candyAlreadyExists;
            }

            await _candiesService.CreateAsync(newCandy);
        }

        public async Task UpdateCandyAsync(string id, Candy updatedCandy)
        {
            var candyAlreadyExists = await VerifyCandyNameAlreadyExist(updatedCandy);
            if (candyAlreadyExists != null)
            {
                throw candyAlreadyExists;
            }

            var candyFound = await _candiesService.GetAsync(id);

            if (candyFound is null)
            {
                throw new HttpException("Doce não encontrado", 404);
            }

            updatedCandy.Id = candyFound.Id;

            await _candiesService.UpdateAsync(id, updatedCandy);
        }

        public async Task RemoveCandyAsync(string id)
        {
            var categoryFound = _candiesService.GetAsync(id);

            if (categoryFound is null)
            {
                throw new HttpException("Doce não encontrado", 404);
            }

            await _candiesService.RemoveAsync(id);
        }

        private async Task<HttpException?> VerifyCandyNameAlreadyExist(Candy candyForVerify)
        {
            var allCandies = await _candiesService.GetAsync();

            if (allCandies.Count > 0)
            {
                bool nameAlreadyExist = allCandies.Exists(category => category.Name == candyForVerify.Name);
                if (nameAlreadyExist)
                {
                    return new HttpException("Doce já existe", 400);
                }
            }

            return null;
        }
    }
}