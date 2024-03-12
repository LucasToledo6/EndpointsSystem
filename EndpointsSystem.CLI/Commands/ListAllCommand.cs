using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;
using EndpointsSystem.CLI.Commands.Outputs;
using System.Text.Json;

namespace EndpointsSystem.CLI.Commands
{
    public class ListAllCommand : BaseCommand
    {
        public override EEndpointCommands Id => EEndpointCommands.ListAllCommand;

        public override string Description => "List all endpoints";

        public override async Task ExecuteCommand()
        {
            var listEndpointsResponse = await _client.GetAsync($"{CommandConfig.ApiUrl}/api/Endpoint/ListAllEndpoints");
            if (!listEndpointsResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to retrieve endpoints.");
                return;
            }

            var responseContent = await listEndpointsResponse.Content.ReadAsStringAsync();
            var endpointsFoundList = JsonSerializer.Deserialize<List<EndpointOutput>>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (endpointsFoundList == null || endpointsFoundList.Count <= 0)
            {
                Console.WriteLine("No endpoints were found.");
                return;
            }

            foreach (var endpoint in endpointsFoundList)
            {
                Console.WriteLine($"Endpoint Serial Number: {endpoint.EndpointSerialNumber}");
                Console.WriteLine($"Meter Model ID: {endpoint.MeterModelId}");
                Console.WriteLine($"Meter Number: {endpoint.MeterNumber}");
                Console.WriteLine($"Meter Firmware Version: {endpoint.MeterFirmwareVersion}");
                Console.WriteLine($"Switch State: {endpoint.SwitchState}");
                Console.WriteLine("---------------------------------------");
            }
        }
    }
}