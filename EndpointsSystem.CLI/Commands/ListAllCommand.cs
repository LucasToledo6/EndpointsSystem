using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands
{
    public class ListAllCommand : BaseCommand
    {
        public override EEndpointCommands Id => EEndpointCommands.ListAllCommand;

        public override string Description => "List all endpoints";

        public void Command()
        {

        }
    }
}