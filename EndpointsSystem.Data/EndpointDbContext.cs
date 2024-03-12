using EndpointsSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EndpointsSystem.Data
{
    public class EndpointDbContext : DbContext
    {
        public EndpointDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Endpoint> Endpoints { get; set; }
    }
}
