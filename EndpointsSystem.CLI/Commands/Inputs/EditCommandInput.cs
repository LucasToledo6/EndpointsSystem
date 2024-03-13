using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands.Inputs
{
    public class EditCommandInput
    {
        public string EndpointSerialNumber { get; set; }
        public ESwitchState SwitchState { get; set; }
    }
}
