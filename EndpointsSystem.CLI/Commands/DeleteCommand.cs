using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands
{
    public class DeleteCommand : BaseCommand
    {
        public override int Id => (int)EEndpointCommands.DeleteCommand;
        public override string Description => "Delete an existing endpoint";
    }
}