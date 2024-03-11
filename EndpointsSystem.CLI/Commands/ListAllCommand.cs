using EndpointsSystem.CLI.Commands.Base;

namespace EndpointsSystem.CLI.Commands
{
    public class ListAllCommand : BaseCommand
    {
        public override int Id => 4;

        public override string Description => "List all endpoints";
    }
}