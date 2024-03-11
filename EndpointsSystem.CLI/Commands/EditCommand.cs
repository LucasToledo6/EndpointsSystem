using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands
{
    public class EditCommand : BaseCommand
    {
        public override int Id => (int)EEndpointCommands.EditCommand;

        public override string Description => "Edit an existing endpoint";
    }
}