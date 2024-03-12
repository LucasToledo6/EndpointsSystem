using EndpointSystem.Application.Input.Model;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EndpointsSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndpointController : ControllerBase
    {
        private readonly IValidator<CreateEndpointInput> _createEndpointInputValidator;

        public EndpointController(IValidator<CreateEndpointInput> createEndpointInputValidator)
        {
            _createEndpointInputValidator = createEndpointInputValidator;
        }

        [HttpPost(nameof(CreateEndpoint))]
        public async Task<IActionResult> CreateEndpoint([FromBody] CreateEndpointInput createCommandInput)
        {
            try
            {
                var validation = _createEndpointInputValidator.Validate(createCommandInput);

                if (!validation.IsValid)
                {
                    throw new ArgumentException("The created endpoint data was invalid.");
                }
                Console.WriteLine();
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unknown error has occurred.");
            }
        }
    }
}