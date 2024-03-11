using EndpointsSystem.CLI.Commands.Base;

namespace EndpointsSystem.CLI.Commands
{
    public class CreateCommand : BaseCommand
    {
        public override int Id => 1;

        public override string Description => "Insert a new endpoint";
    }
}