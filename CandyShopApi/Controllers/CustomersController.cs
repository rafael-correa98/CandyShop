using CandyShopApi.Models;
using CandyShopApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CandyShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomersService _customersService;

        public CustomersController(CustomersService customersService) =>
            _customersService = customersService;

        [HttpGet]
        public async Task<List<Customer>> Get() =>
            await _customersService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Customer>> Get(string id)
        {
            var customer = await _customersService.GetAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Customer newCustomer)
        {
            await _customersService.CreateAsync(newCustomer);

            return CreatedAtAction(nameof(Get), new { id = newCustomer.Id }, newCustomer);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Customer updatedCustomer)
        {
            var customer = await _customersService.GetAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            updatedCustomer.Id = customer.Id;

            await _customersService.UpdateAsync(id, updatedCustomer);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var customer = await _customersService.GetAsync(id);

            if (customer is null)
            {
                return NotFound();
            }

            await _customersService.RemoveAsync(id);

            return NoContent();
        }
    }
}
