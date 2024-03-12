using EndpointSystem.Application.DTO;
using EndpointSystem.Application.Input.Model;

namespace EndpointSystem.Application.Services.Interfaces
{
    public interface IEndpointService
    {
        public Task CreateEndpoint(CreateEndpointInput createEndpointInput);
        public Task<EndpointDto> FindEndpoint(string endpointSerialNumber);
        public Task<List<EndpointDto>> ListAllEndpoints();
    }
}
