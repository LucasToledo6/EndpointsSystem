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

            var findEndpointResponse = await _client.GetAsync($"{CommandConfig.ApiUrl}/api/Endpoint/FindEndpoint/{endpointSerialNumber}");
            if (!findEndpointResponse.IsSuccessStatusCode)
            {
                var errorContent = await findEndpointResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"{errorContent}");
                return;
            }

            string confirmation = ReadConfirmation();
            if (!confirmation.Equals("y", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Deletion command aborted.");
                return;
            }

            var deleteEndpointResponse = await _client.DeleteAsync($"{CommandConfig.ApiUrl}/api/Endpoint/DeleteEndpoint/{endpointSerialNumber}");
            if (!deleteEndpointResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to delete endpoint.");
                return;
            }

            Console.WriteLine("Endpoint successfully deleted.");
        }
    }
}