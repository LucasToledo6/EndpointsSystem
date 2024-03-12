using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;
using EndpointsSystem.CLI.Commands.Extensions;
using System.Net.Http.Json;

namespace EndpointsSystem.CLI.Commands
{
    public class EditCommand : BaseCommand
    {
        public override EEndpointCommands Id => EEndpointCommands.EditCommand;

        public override string Description => "Edit an existing endpoint";

        public override async Task ExecuteCommand()
        {
            string endpointSerialNumber = ReadEndpointSerialNumber();

            var findEndpointResponse = await _client.GetAsync($"{CommandConfig.ApiUrl}/api/Endpoint/{endpointSerialNumber}");
            if (!findEndpointResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Endpoint was not found.");
                return;
            }

            ESwitchState newSwitchState = ReadNewSwitchState();
            var editEndpointResponse = await _client.PutAsJsonAsync($"{CommandConfig.ApiUrl}/api/Endpoint/{endpointSerialNumber}", newSwitchState);
            if (!editEndpointResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("The endpoint could not be edited.");
                return;
            }

            Console.WriteLine("Endpoint edited successfully.");
        }

        private string ReadEndpointSerialNumber()
        {
            Console.WriteLine("Please, enter the serial number.");
            string endpointSerialNumber = CheckString();
            return endpointSerialNumber;
        }

        private ESwitchState ReadNewSwitchState()
        {
            int switchState;

            do
            {
                Console.WriteLine("Please, enter the new meter switch state.");
                Console.WriteLine("The available states are:");
                foreach (var state in SwitchStateExtensions.GetSwitchStates())
                {
                    Console.WriteLine($"{(int)state}) {state}");
                }
                switchState = CheckInt();
            } while (!SwitchStateExtensions.IsSwitchStateValid(switchState));

            return (ESwitchState)switchState;
        }
    }
}