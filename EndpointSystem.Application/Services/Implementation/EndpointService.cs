using AutoMapper;
using EndpointsSystem.Data.Repository.Interface;
using EndpointsSystem.Domain.Entities;
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

            var endpoint = _mapper.Map<Endpoint>(createEndpointInput);

            await _endpointRepository.Create(endpoint);
            await _endpointRepository.SaveAsync();
        }
    }
}
