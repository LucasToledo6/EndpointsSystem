﻿using EndpointsSystem.Data.Repository.Interface;
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
            await _db.Endpoints.AddAsync(endpoint);
        }

        public async Task Update(Endpoint endpoint)
        {
            _db.Endpoints.Update(endpoint);
        }

        public async Task Delete(Endpoint endpoint)
        {
            _db.Endpoints.Remove(endpoint);
        }

        public async Task<Endpoint?> GetEndpointBySerialNumberAsync(string endpointSerialNumber)
        {
            return await _db.Endpoints.AsNoTracking().FirstOrDefaultAsync(x => x.EndpointSerialNumber == endpointSerialNumber);
        }

        public async Task<List<Endpoint>> GetAllEndpoints()
        {
            return await _db.Endpoints.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
