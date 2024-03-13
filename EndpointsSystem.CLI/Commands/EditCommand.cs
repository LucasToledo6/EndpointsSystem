using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;
using EndpointsSystem.CLI.Commands.Extensions;
using EndpointsSystem.CLI.Commands.Inputs;
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
            ESwitchState newSwitchState = ReadNewSwitchState();

            EditCommandInput editCommandInput = new EditCommandInput
            {
                EndpointSerialNumber = endpointSerialNumber,
                SwitchState = newSwitchState
            };

            var editEndpointResponse = await _client.PutAsJsonAsync($"{CommandConfig.ApiUrl}/api/Endpoint/EditEndpoint/{endpointSerialNumber}", editCommandInput);
            
            if (!editEndpointResponse.IsSuccessStatusCode)
            {
                var errorContent = await editEndpointResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"{errorContent}");
                return;
            }

            Console.WriteLine("Endpoint edited successfully.");
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