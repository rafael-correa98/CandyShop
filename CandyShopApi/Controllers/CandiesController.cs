using CandyShopApi.BusinessRules;
using CandyShopApi.BusinessRules.Interface;
using CandyShopApi.Errors;
using CandyShopApi.Models;
using CandyShopApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CandyShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandiesController : ControllerBase
    {
        private readonly CandiesBusiness _candiesBusiness;
        private readonly CandiesService _candiesService;

        public CandiesController(CandiesBusiness candiesBusiness, CandiesService candiesService)
        {
            _candiesBusiness = candiesBusiness;
        }

        [HttpGet]
        public async Task<List<Candy>> Get()
        {
            try
            {
                var allCandies = await _candiesBusiness.GetAllCandiesAsync();
                return allCandies;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Candy>> Get(string id)
        {
            try
            {
                return await _candiesBusiness.GetCandyByIdAsync(id);
            }
            catch (HttpException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Candy newCandy)
        {
            try
            {
                await _candiesBusiness.CreateCandyAsync(newCandy);

                return CreatedAtAction(nameof(Get), new { id = newCandy.Id }, newCandy);
            }
            catch (HttpException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Candy updatedCandy)
        {
            try
            {
                await _candiesBusiness.UpdateCandyAsync(id, updatedCandy);

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
                await _candiesBusiness.RemoveCandyAsync(id);

                return NoContent();
            }
            catch (HttpException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }
    }
}
