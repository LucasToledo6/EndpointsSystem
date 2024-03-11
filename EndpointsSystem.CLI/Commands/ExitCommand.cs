using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands
{
    public class ExitCommand : BaseCommand
    {
        public override EEndpointCommands Id => EEndpointCommands.ExitCommand;

        public override string Description => "Exit the system";

        public string Command()
        {
            Console.WriteLine("Are you sure you want to exit the application?");
            string confirmation = CheckString();
        }
    }
}