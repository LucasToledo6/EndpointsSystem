using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands
{
    public class CreateCommand : BaseCommand
    {
        public override int Id => (int)EEndpointCommands.CreateCommand;

        public override string Description => "Insert a new endpoint";
    }
}