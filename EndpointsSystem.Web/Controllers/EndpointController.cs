using EndpointsSystem.Domain.Enums;
using EndpointSystem.Application.Input.Model;
using EndpointSystem.Application.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EndpointsSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndpointController : ControllerBase
    {
        private readonly IValidator<CreateEndpointInput> _createEndpointInputValidator;
        private readonly IValidator<EditEndpointInput> _editEndpointInputValidator;
        private readonly IEndpointService _endpointService;

        public EndpointController(
            IValidator<CreateEndpointInput> createEndpointInputValidator,
            IValidator<EditEndpointInput> editEndpointInputValidator,
            IEndpointService endpointService)
        {
            _createEndpointInputValidator = createEndpointInputValidator;
            _editEndpointInputValidator = editEndpointInputValidator;
            _endpointService = endpointService;
        }

        [HttpPost(nameof(CreateEndpoint))]
        public async Task<IActionResult> CreateEndpoint([FromBody] CreateEndpointInput createCommandInput)
        {
            try
            {
                // Server-side validation
                var validation = _createEndpointInputValidator.Validate(createCommandInput);

                if (!validation.IsValid)
                {
                    throw new ArgumentException("The created endpoint data was invalid.");
                }

                await _endpointService.CreateEndpoint(createCommandInput);
                Console.WriteLine();
                return Ok();
            }
            catch (ArgumentException ex) //for expected errors
            {
                return BadRequest(ex.Message);
            }
            catch (Exception) //for unexpected errors
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unknown error has occurred.");
            }
        }

        [HttpPut("EditEndpoint/{endpointSerialNumber}")]
        public async Task<IActionResult> EditEndpoint([FromRoute] string endpointSerialNumber, [FromBody] EditEndpointInput editEndpointInput)
        {
            try
            {
                var validation = _editEndpointInputValidator.Validate(editEndpointInput);

                if (!validation.IsValid)
                {
                    throw new ArgumentException("The edited endpoint data was invalid.");
                }

                await _endpointService.EditEndpoint(editEndpointInput);
                Console.WriteLine();
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unknown error has occurred.");
            }
        }

        [HttpDelete("DeleteEndpoint/{endpointSerialNumber}")]
        public async Task<IActionResult> DeleteEndpoint([FromRoute] string endpointSerialNumber)
        {
            try
            {
                await _endpointService.DeleteEndpoint(endpointSerialNumber);
                Console.WriteLine();
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unknown error has occurred.");
            }
        }

        [HttpGet("FindEndpoint/{endpointSerialNumber}")]
        public async Task<IActionResult> FindEndpoint([FromRoute] string endpointSerialNumber)
        {
            try
            {
                return Ok(await _endpointService.FindEndpoint(endpointSerialNumber!));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unknown error has occurred.");
            }
        }

        [HttpGet(nameof(ListAllEndpoints))]
        public async Task<IActionResult> ListAllEndpoints()
        {
            try
            {
                return Ok(await _endpointService.ListAllEndpoints());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unknown error has occurred.");
            }
        }
    }
}