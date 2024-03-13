using ANSIConsole;
using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;
using System.Text.Json;

namespace EndpointsSystem.CLI.Commands
{
    public class DeleteCommand : BaseCommand
    {
        public override EEndpointCommands Id => EEndpointCommands.DeleteCommand;
        public override string Description => "Delete".Underlined() + " an existing endpoint";

        public override async Task ExecuteCommand()
        {
            string endpointSerialNumber = ReadEndpointSerialNumber();
            string confirmation = ReadConfirmation();

            // Focused on trying to reduce the likelihood of accidental data loss or other unintended consequences
            if (!confirmation.Equals("y", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Deletion command aborted.".Color("Red"));
                return;
            }

            var deleteEndpointResponse = await _client.DeleteAsync($"{CommandConfig.ApiUrl}/api/Endpoint/DeleteEndpoint/{endpointSerialNumber}");
            
            if (!deleteEndpointResponse.IsSuccessStatusCode)
            {
                var errorContentJson = await deleteEndpointResponse.Content.ReadAsStringAsync();
                var errorContent = JsonSerializer.Deserialize<string>(errorContentJson);
                Console.WriteLine(errorContent.Color("Red"));
                return;
            }

            Console.WriteLine("Endpoint successfully deleted.".Color("Lightgreen"));
        }

        private string ReadConfirmation()
        {
            Console.WriteLine("Are you sure you want to " + "delete".Color("Red").Bold() + " this endpoint? " + "(y/n)".Color("Red").Bold());
            string confirmation = CheckString();
            Console.WriteLine();
            return confirmation;
        }
    }
}