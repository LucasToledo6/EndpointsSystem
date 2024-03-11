using Microsoft.AspNetCore.Mvc;

namespace EndpointsSystem.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndpointController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateEndpoint([FromBody] )
        {

        }
    }
}