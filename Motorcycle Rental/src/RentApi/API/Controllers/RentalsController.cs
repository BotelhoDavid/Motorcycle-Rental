using Microsoft.AspNetCore.Mvc;
using Rent.Application.Interfaces;
using Rent.Application.ViewModels;

namespace Motorcycle_Rental.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentAppService _rentAppService;

        public RentalsController(IRentAppService rentAppService)
        {
            _rentAppService = rentAppService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRental([FromBody] CreateRentalViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _rentAppService.CreateAsync(model);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var rental = await _rentAppService.GetAsync(id);
            if (rental == null)
                return NotFound();

            return Ok(rental);
        }

        [HttpPut("{id}/devolution")]
        public async Task<IActionResult> SetDevolution(Guid id, [FromBody] RentalDevolutionViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _rentAppService.SetDevolutionAsync(id, model);

            return Ok();
        }
    }
}
}
