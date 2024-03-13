using ANSIConsole;
using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;
using EndpointsSystem.CLI.Commands.Outputs;
using System.Text.Json;

namespace EndpointsSystem.CLI.Commands
{
    public class ListAllCommand : BaseCommand
    {
        public override EEndpointCommands Id => EEndpointCommands.ListAllCommand;

        public override string Description => "List all".Underlined() + " endpoints";

        public override async Task ExecuteCommand()
        {
            var listEndpointsResponse = await _client.GetAsync($"{CommandConfig.ApiUrl}/api/Endpoint/ListAllEndpoints");
            
            if (!listEndpointsResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to retrieve endpoints.".Color("Red"));
                return;
            }

            var responseContent = await listEndpointsResponse.Content.ReadAsStringAsync();
            var endpointsFoundList = JsonSerializer.Deserialize<List<EndpointOutput>>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (endpointsFoundList == null || endpointsFoundList.Count <= 0)
            {
                Console.WriteLine("No endpoints were found.".Color("Red"));
                return;
            }

            foreach (var endpoint in endpointsFoundList)
            {
                Console.WriteLine("Endpoint Serial Number".Color("Lightgreen") + $": {endpoint.EndpointSerialNumber}");
                Console.WriteLine("Meter Model ID".Color("Lightgreen") + $": {endpoint.MeterModelId}");
                Console.WriteLine("Meter Number".Color("Lightgreen") + $": {endpoint.MeterNumber}");
                Console.WriteLine("Meter Firmware Version".Color("Lightgreen") + $": {endpoint.MeterFirmwareVersion}");
                Console.WriteLine("Switch State".Color("Lightgreen") + $": {endpoint.SwitchState}");
                Console.WriteLine("---------------------------------------");
            }
        }
    }
}