﻿using EndpointSystem.Application.Input.Model;
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
        private readonly IEndpointService _endpointService;

        public EndpointController(
            IValidator<CreateEndpointInput> createEndpointInputValidator,
            IEndpointService endpointService)
        {
            _createEndpointInputValidator = createEndpointInputValidator;
            _endpointService = endpointService;
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

                await _endpointService.CreateEndpoint(createCommandInput);
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