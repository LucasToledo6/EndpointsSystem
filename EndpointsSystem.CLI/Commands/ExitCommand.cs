using ANSIConsole;
using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands
{
    public class ExitCommand : BaseCommand
    {
        public override EEndpointCommands Id => EEndpointCommands.ExitCommand;

        public override string Description => "Exit".Underlined() + " the system";

        public override async Task ExecuteCommand()
        {
            string confirmation = ReadConfirmation();

            if (!confirmation.Equals("y", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Exit command aborted.".Color("Red"));
                return;
            }

            Console.WriteLine("Exiting...".Color("Lightgreen"));
            Environment.Exit(0);
        }

        private string ReadConfirmation()
        {
            Console.WriteLine("Are you sure you want to " + "exit".Color("Red").Bold() + " the application? " + "(y/n)".Color("Red").Bold());
            string confirmation = CheckString();
            Console.WriteLine();
            return confirmation;
        }
    }
}