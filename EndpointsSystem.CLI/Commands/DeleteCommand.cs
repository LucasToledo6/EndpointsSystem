using EndpointsSystem.CLI.Commands.Base;

namespace EndpointsSystem.CLI.Commands
{
    public class DeleteCommand : BaseCommand
    {
        public override int Id => 3;
        public override string Description => "Delete an existing endpoint";
    }
}