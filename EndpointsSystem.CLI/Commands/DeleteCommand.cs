using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands
{
    public class DeleteCommand : BaseCommand
    {
        public override int Id => (int)EEndpointCommands.DeleteCommand;
        public override string Description => "Delete an existing endpoint";

        public string Command()
        {
            Console.WriteLine("Please, enter the endpoint serial number.");
            int endpointSerialNumber = CheckInt();

            Console.WriteLine("Are you sure you want to delete this endpoint?");
            string confirmation = CheckString();
        }
    }
}