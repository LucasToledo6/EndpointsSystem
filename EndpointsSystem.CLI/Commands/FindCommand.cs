using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;
using EndpointsSystem.CLI.Commands.Outputs;
using System.Text.Json;

namespace EndpointsSystem.CLI.Commands
{
    public class FindCommand : BaseCommand
    {
        public override EEndpointCommands Id => EEndpointCommands.FindCommand;

        public override string Description => "Find a endpoint by serial number";

        public override async Task ExecuteCommand()
        {
            string endpointSerialNumber = ReadEndpointSerialNumber();

            var findEndpointResponse = await _client.GetAsync($"{CommandConfig.ApiUrl}/api/Endpoint/{endpointSerialNumber}");
            if (!findEndpointResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Endpoint was not found.");
                return;
            }

            var responseContent = await findEndpointResponse.Content.ReadAsStringAsync();
            var endpointFound = JsonSerializer.Deserialize<EndpointOutput>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (endpointFound == null)
            {
                Console.WriteLine("Failed to extract the endpoint information.");
                return;
            }
            // Display the endpoint information
            Console.WriteLine($"Endpoint Serial Number: {endpointFound.EndpointSerialNumber}");
            Console.WriteLine($"Meter Model ID: {endpointFound.MeterModelId}");
            Console.WriteLine($"Meter Number: {endpointFound.MeterNumber}");
            Console.WriteLine($"Meter Firmware Version: {endpointFound.MeterFirmwareVersion}");
            Console.WriteLine($"Switch State: {endpointFound.SwitchState}");
        }

        private string ReadEndpointSerialNumber()
        {
            Console.WriteLine("Please, enter the serial number.");
            string endpointSerialNumber = CheckString();
            return endpointSerialNumber;
        }
    }
}