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
            string confirmation = ReadConfirmation();

            // Focused on trying to reduce the likelihood of accidental data loss or other unintended consequences
            if (!confirmation.Equals("y", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Deletion command aborted.");
                return;
            }

            var deleteEndpointResponse = await _client.DeleteAsync($"{CommandConfig.ApiUrl}/api/Endpoint/DeleteEndpoint/{endpointSerialNumber}");
            
            if (!deleteEndpointResponse.IsSuccessStatusCode)
            {
                var errorContent = await deleteEndpointResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"{errorContent}");
                return; 
            }

            Console.WriteLine("Endpoint successfully deleted.");
        }
    }
}