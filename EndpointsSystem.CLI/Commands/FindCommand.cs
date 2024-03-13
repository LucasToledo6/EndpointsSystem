using ANSIConsole;
using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;
using EndpointsSystem.CLI.Commands.Outputs;
using System.Text.Json;

namespace EndpointsSystem.CLI.Commands
{
    public class FindCommand : BaseCommand
    {
        public override EEndpointCommands Id => EEndpointCommands.FindCommand;

        public override string Description => "Find".Underlined() + " a endpoint by serial number";

        public override async Task ExecuteCommand()
        {
            string endpointSerialNumber = ReadEndpointSerialNumber();
            
            var findEndpointResponse = await _client.GetAsync($"{CommandConfig.ApiUrl}/api/Endpoint/FindEndpoint/{endpointSerialNumber}");
            
            if (!findEndpointResponse.IsSuccessStatusCode)
            {
                var errorContentJson = await findEndpointResponse.Content.ReadAsStringAsync();
                var errorContent = JsonSerializer.Deserialize<string>(errorContentJson);
                Console.WriteLine(errorContent.Color("Red"));
                return;
            }

            var responseContent = await findEndpointResponse.Content.ReadAsStringAsync();
            var endpointFound = JsonSerializer.Deserialize<EndpointOutput>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Display the endpoint information
            Console.WriteLine("Endpoint Serial Number".Color("Lightgreen") + $": {endpointFound.EndpointSerialNumber}");
            Console.WriteLine("Meter Model ID".Color("Lightgreen") + $": {endpointFound.MeterModelId}");
            Console.WriteLine("Meter Number".Color("Lightgreen") + $": {endpointFound.MeterNumber}");
            Console.WriteLine("Meter Firmware Version".Color("Lightgreen") + $": {endpointFound.MeterFirmwareVersion}");
            Console.WriteLine("Switch State".Color("Lightgreen") + $": {endpointFound.SwitchState}");
        }
    }
}