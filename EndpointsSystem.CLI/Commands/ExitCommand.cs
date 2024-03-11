using EndpointsSystem.CLI.Commands.Base;

namespace EndpointsSystem.CLI.Commands
{
    public class ExitCommand : BaseCommand
    {
        public override int Id => 6;

        public override string Description => "Exit the system";
    }
}