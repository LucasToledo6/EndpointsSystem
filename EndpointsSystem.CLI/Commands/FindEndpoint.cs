using EndpointsSystem.CLI.Commands.Base;

namespace EndpointsSystem.CLI.Commands
{
    public class FindEndpointCommand : BaseCommand
    {
        public override int Id => 5;

        public override string Description => "Find a endpoint by serial number";
    }
}