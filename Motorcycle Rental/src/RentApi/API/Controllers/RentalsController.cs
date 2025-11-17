using Microsoft.AspNetCore.Mvc;
using Rent.Application.Interfaces;
using Rent.Application.ViewModels;

namespace Motorcycle_Rental.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentAppService _service;

        public RentalsController(IRentAppService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRentalViewModel rental)
        {
            MessageViewModel message = await _service.CreateAsync(rental);

            if (message.Success)
                return Created();
            else
                return BadRequest(new MessageViewModel("Invalid data"));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(string id)
        {
            if (!Guid.TryParse(id, out Guid guid))
            {
                return BadRequest(new MessageViewModel("Malformed request"));
            }

            RentalViewModel rent = await _service.GetAsync(guid);

            if (rent.Success)
                return Ok(rent);
            else
                return NotFound(new MessageViewModel("Moto not found"));
        }

        [HttpPut("{id}/Return")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] RentalReturnViewModel model)
        {
            if (!Guid.TryParse(id, out Guid guid))
            {
                return BadRequest(new MessageViewModel("Invalid data"));
            }
            MessageViewModel message = await _service.SetReturnAsync(guid, model);

            if (message.Success)
                return Ok(message);
            else
                return BadRequest(message);
        }
    }
}
