using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands
{
    public class ExitCommand : BaseCommand
    {
        public override EEndpointCommands Id => EEndpointCommands.ExitCommand;

        public override string Description => "Exit the system";

        public override async Task ExecuteCommand()
        {
            string confirmation = ReadConfirmation();

            if (!confirmation.Equals("y", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Exit command aborted.");
                return;
            }

            Console.WriteLine("Exiting...");
            Environment.Exit(0);
        }

        private string ReadConfirmation()
        {
            Console.WriteLine("Are you sure you want to exit the application? (y/N)");
            string confirmation = CheckString();
            return confirmation;
        }
    }
}