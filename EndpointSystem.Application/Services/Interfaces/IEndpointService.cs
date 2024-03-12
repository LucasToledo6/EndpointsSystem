using EndpointSystem.Application.Input.Model;

namespace EndpointSystem.Application.Services.Interfaces
{
    public interface IEndpointService
    {
        public Task CreateEndpoint(CreateEndpointInput createEndpointInput);
    }
}
