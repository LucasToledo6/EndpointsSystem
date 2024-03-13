using AutoMapper;
using EndpointsSystem.Data.Repository.Interface;
using EndpointsSystem.Domain.Entities;
using EndpointsSystem.Domain.Enums;
using EndpointSystem.Application.DTO;
using EndpointSystem.Application.Input.Model;
using EndpointSystem.Application.Services.Interfaces;

namespace EndpointSystem.Application.Services.Implementation
{
    public class EndpointService : IEndpointService
    {
        private readonly IEndpointRepository _endpointRepository;
        private readonly IMapper _mapper;

        public EndpointService(
            IEndpointRepository endpointRepository,
            IMapper mapper)
        {
            _endpointRepository = endpointRepository;
            _mapper = mapper;
        }

        public async Task CreateEndpoint(CreateEndpointInput createEndpointInput)
        {
            var existingEndpoint = await _endpointRepository.GetEndpointBySerialNumberAsync(createEndpointInput.EndpointSerialNumber!);

            if (existingEndpoint != null)
            {
                throw new ArgumentException("An endpoint already exists with this serial number.");
            }

            // Using AutoMapper to automate the mapping of one object to another
            // Map properties from an instance of createEndpointInput to a new instance of Endpoint
            var endpoint = _mapper.Map<Endpoint>(createEndpointInput);

            await _endpointRepository.Create(endpoint);
            await _endpointRepository.SaveAsync();
        }

        public async Task EditEndpoint(EditEndpointInput editCommandInput)
        {
            var existingEndpoint = await _endpointRepository.GetEndpointBySerialNumberAsync(editCommandInput.EndpointSerialNumber!);

            if (existingEndpoint == null)
            {
                throw new ArgumentException("The endpoint was not found.");
            }
            
            if (existingEndpoint.SwitchState == editCommandInput.SwitchState)
            {
                throw new ArgumentException("The new switch state is the same as the current switch state. No changes made.");
            }

            existingEndpoint!.SwitchState = editCommandInput.SwitchState;

            await _endpointRepository.Update(existingEndpoint);
            await _endpointRepository.SaveAsync();
        }

        public async Task DeleteEndpoint(string endpointSerialNumber)
        {
            var existingEndpoint = await _endpointRepository.GetEndpointBySerialNumberAsync(endpointSerialNumber);

            if (existingEndpoint == null)
            {
                throw new ArgumentException("The endpoint was not found.");
            }

            await _endpointRepository.Delete(existingEndpoint);
            await _endpointRepository.SaveAsync();
        }

        public async Task<EndpointDto> FindEndpoint(string endpointSerialNumber)
        {
            var existingEndpoint = await _endpointRepository.GetEndpointBySerialNumberAsync(endpointSerialNumber) ?? throw new ArgumentException("The endpoint was not found.");

            // Using a DTO to further decouple presentation from the service layer and the domain model
            var foundEndpoint = _mapper.Map<EndpointDto>(existingEndpoint);

            return foundEndpoint;
        }

        public async Task<List<EndpointDto>> ListAllEndpoints()
        {
            List<Endpoint> endpointList = await _endpointRepository.GetAllEndpoints();

            var endpointDtoList = _mapper.Map<List<EndpointDto>>(endpointList);

            return endpointDtoList;
        }
    }
}
