using EndpointsSystem.Domain.Enums;

namespace EndpointSystem.Application.Input.Model
{
    public class EditEndpointInput
    {
        public string? EndpointSerialNumber { get; set; }
        public ESwitchState SwitchState { get; set; }
    }
}
