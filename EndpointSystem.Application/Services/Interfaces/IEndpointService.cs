using EndpointsSystem.Domain.Enums;
using EndpointSystem.Application.DTO;
using EndpointSystem.Application.Input.Model;

namespace EndpointSystem.Application.Services.Interfaces
{
    public interface IEndpointService
    {
        public Task CreateEndpoint(CreateEndpointInput createEndpointInput);
        public Task EditEndpoint(EditEndpointInput editEndpointInput);
        public Task DeleteEndpoint(string endpointSerialNumber);
        public Task<EndpointDto> FindEndpoint(string endpointSerialNumber);
        public Task<List<EndpointDto>> ListAllEndpoints();
    }
}
