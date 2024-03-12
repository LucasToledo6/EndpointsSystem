using EndpointsSystem.Data.Repository.Interface;
using EndpointsSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EndpointsSystem.Data.Repository.Implementation
{
    public class EndpointRepository : IEndpointRepository
    {
        private EndpointDbContext _db;

        public EndpointRepository(EndpointDbContext db)
        {
            _db = db;
        }

        public async Task Create(Endpoint endpoint)
        {
            await _db.AddAsync(endpoint);
        }

        //public void Update()
        //{
        //}

        //public void Delete()
        //{
        //}

        public async Task<Endpoint?> GetEndpointBySerialNumberAsync(string endpointSerialNumber)
        {
            return await _db.Endpoints.AsNoTracking().FirstOrDefaultAsync(x => x.EndpointSerialNumber == endpointSerialNumber);
        }

        //public async Task<> GetAllEndpoints()
        //{
        //}

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
