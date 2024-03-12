using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands
{
    public class DeleteCommand : BaseCommand
    {
        public override EEndpointCommands Id => EEndpointCommands.DeleteCommand;
        public override string Description => "Delete an existing endpoint";

        public override async Task ExecuteCommand()
        {
            string endpointSerialNumber = ReadEndpointSerialNumber();

            var findEndpointResponse = await _client.GetAsync($"{CommandConfig.ApiUrl}/api/Endpoint/{endpointSerialNumber}");
            if (!findEndpointResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Endpoint was not found.");
                return;
            }

            string confirmation = ReadConfirmation();
            if (!confirmation.Equals("y", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Deletion command aborted.");
                return;
            }

            var deleteEndpointResponse = await _client.DeleteAsync($"{CommandConfig.ApiUrl}/api/Endpoint/{endpointSerialNumber}");
            if (!deleteEndpointResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("The endpoint could not be deleted.");
                return;
            }

            Console.WriteLine("Endpoint successfully deleted.");
        }

        private string ReadEndpointSerialNumber()
        {
            Console.WriteLine("Please, enter the serial number.");
            string endpointSerialNumber = CheckString();
            return endpointSerialNumber;
        }

        private string ReadConfirmation()
        {
            Console.WriteLine("Are you sure you want to delete this endpoint? (y/N)");
            string confirmation = CheckString();
            return confirmation;
        }
    }
}