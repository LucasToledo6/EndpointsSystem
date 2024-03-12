using EndpointSystem.Application.DTO;
using EndpointSystem.Application.Input.Model;

namespace EndpointSystem.Application.Services.Interfaces
{
    public interface IEndpointService
    {
        public Task CreateEndpoint(CreateEndpointInput createEndpointInput);
        public Task<List<EndpointDto>> ListAllEndpoints();
    }
}
