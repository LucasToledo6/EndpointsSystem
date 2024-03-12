namespace EndpointsSystem.Data.Repository
{
    public class EndpointRepository
    {
        private EndpointDbContext _endpointContext;

        public EndpointRepository(EndpointDbContext endpointContext)
        {
            _endpointContext = endpointContext;
        }

        //public void Create()
        //{
        //}

        //public void Update()
        //{
        //}

        //public void Delete()
        //{
        //}

        //public async Task<> GetEndpointBySerialNumberAsync()
        //{
        //}

        //public async Task<> GetAllEndpoints()
        //{
        //}

        //public async Task SaveAsync()
        //{
        //    await _endpointContext.SaveChangesAsync();
        //}
    }
}
