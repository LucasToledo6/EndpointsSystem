using EndpointsSystem.CLI.Commands.Base;

namespace EndpointsSystem.CLI.Commands
{
    public class EditCommand : BaseCommand
    {
        public override int Id => 2;

        public override string Description => "Edit an existing endpoint";
    }
}