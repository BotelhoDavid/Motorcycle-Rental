using Microsoft.AspNetCore.Mvc;
using Rent.Application.Interfaces;
using Rent.Application.ViewModels;

namespace Motorcycle_Rental.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriverAppService _service;

        public DriversController(IDriverAppService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateDriverViewModel driver)
        {
            MessageViewModel message = await _service.CreateAsync(driver);

            if (message.Success)
                return Created();
            else
                return BadRequest(new MessageViewModel("Invalid data"));
        }
    }
}
