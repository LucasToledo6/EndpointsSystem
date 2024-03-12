using EndpointsSystem.Domain.Entities;

namespace EndpointsSystem.Data.Repository.Interface
{
    public interface IEndpointRepository
    {
        public Task Create(Endpoint endpoint);
        public Task Update(Endpoint endpoint);
        public Task Delete(Endpoint endpoint);
        public Task<Endpoint?> GetEndpointBySerialNumberAsync(string endpointSerialNumber);
        public Task<List<Endpoint>> GetAllEndpoints();
        public Task SaveAsync();
    }
}
