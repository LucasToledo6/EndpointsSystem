using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands.Inputs
{
    public class CreateCommandInput
    {
        public string EndpointSerialNumber { get; set; }
        public EMeterModelId MeterModelId { get; set; }
        public int MeterNumber { get; set; }
        public string MeterFirmwareVersion { get; set; }
        public ESwitchState SwitchState { get; set; }
    }
}
