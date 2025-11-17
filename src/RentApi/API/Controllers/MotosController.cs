using Microsoft.AspNetCore.Mvc;
using Rent.Application.Interfaces;
using Rent.Application.ViewModels;

namespace Motorcycle_Rental.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MotosController : ControllerBase
    {
        private readonly IMotoAppService _service;

        public MotosController(IMotoAppService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMotoViewModel moto)
        {
            MessageViewModel message = await _service.CreateAsync(moto);

            if (message.Success)
                return Created();
            else
                return BadRequest(new MessageViewModel("Invalid data"));
        }

        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(string id)
        {
            if (!Guid.TryParse(id, out Guid guid))
            {
                return BadRequest(new MessageViewModel("Malformed request"));
            }

            MotoReturnViewModel moto = await _service.GetAsync(guid);

            if (moto.Success)
                return Ok(moto);
            else
                return NotFound(new MessageViewModel("Moto not found"));
        }

        [HttpPut("{id}/placa")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] UpdateMotoViewModel moto)
        {
            if (!Guid.TryParse(id, out Guid guid))
            {
                return BadRequest(new MessageViewModel("Malformed request"));
            }

            MessageViewModel message = await _service.UpdateAsync(guid, moto);

            if (message.Success)
                return Ok(message);
            else
                return BadRequest(message);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (!Guid.TryParse(id, out Guid guid))
            {
                return BadRequest(new MessageViewModel("Invalid data"));
            }

            var message = await _service.DeleteAsync(guid);

            if (message.Success)
                return Ok(message);

            else
                return BadRequest(message);
        }
    }
}