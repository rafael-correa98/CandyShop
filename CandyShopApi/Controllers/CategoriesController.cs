using CandyShopApi.BusinessRules;
using CandyShopApi.BusinessRules.Interface;
using CandyShopApi.Errors;
using CandyShopApi.Models;
using CandyShopApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CandyShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesBusiness _categoriesBusiness;

        public CategoriesController(CategoriesBusiness categoriesBusiness)
        {
            _categoriesBusiness = categoriesBusiness;
        }


        [HttpGet]
        public async Task<List<Category>> Get()
        {
            try
            {
                var allCategories = await _categoriesBusiness.GetAllCategoriesAsync();
                return allCategories;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Category>> Get(string id)
        {
            try
            {
                return await _categoriesBusiness.GetCategoryByIdAsync(id);
            }
            catch (HttpException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Category newCategory)
        {
            try
            {
                await _categoriesBusiness.CreateCategoryAsync(newCategory);

                return CreatedAtAction(nameof(Get), new { id = newCategory.Id }, newCategory);
            }
            catch (HttpException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Category updatedCategory)
        {
            try
            {
                await _categoriesBusiness.UpdateCategoryAsync(id, updatedCategory);

                return NoContent();
            }
            catch (HttpException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _categoriesBusiness.RemoveCategoryAsync(id);

                return NoContent();
            }
            catch (HttpException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }
    }
}
