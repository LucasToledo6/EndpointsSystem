using EndpointsSystem.Domain.Enums;

namespace EndpointSystem.Application.Input.Model
{
    public class CreateEndpointInput
    {
        public string? EndpointSerialNumber { get; set; }
        public EMeterModelId MeterModelId { get; set; }
        public int MeterNumber { get; set; }
        public string? MeterFirmwareVersion { get; set; }
        public ESwitchState SwitchState { get; set; }
    }
}
