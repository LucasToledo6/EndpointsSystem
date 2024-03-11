using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands
{
    public class ListAllCommand : BaseCommand
    {
        public override int Id => (int)EEndpointCommands.ListAllCommand;

        public override string Description => "List all endpoints";
    }
}