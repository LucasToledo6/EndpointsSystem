using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;
using EndpointsSystem.CLI.Commands.Extensions;
using EndpointsSystem.CLI.Commands.Inputs;
using System.Net.Http.Json;

namespace EndpointsSystem.CLI.Commands
{
    public class CreateCommand : BaseCommand
    {
        public override EEndpointCommands Id => EEndpointCommands.CreateCommand;

        public override string Description => "Insert a new endpoint";

        public override async Task ExecuteCommand()
        {
            CreateCommandInput createCommandEndpointInput = new CreateCommandInput
            {
                EndpointSerialNumber = ReadEndpointSerialNumber(),
                MeterModelId = ReadMeterModelId(),
                MeterNumber = ReadMeterNumber(),
                MeterFirmwareVersion = ReadMeterFirmwareVersion(),
                SwitchState = ReadSwitchState()
            };

            var createEndpointResponse = await _client.PostAsJsonAsync($"{CommandConfig.ApiUrl}/api/Endpoint/CreateEndpoint", createCommandEndpointInput);

            if (!createEndpointResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("An error has occurred.");
                return;
            }

            Console.WriteLine("Endpoint created successfully.");
        }

        private string ReadEndpointSerialNumber()
        {
            Console.WriteLine("Please, enter the serial number.");
            string endpointSerialNumber = CheckString();
            return endpointSerialNumber;
        }

        private EMeterModelId ReadMeterModelId()
        {
            int meterModelId;

            do
            {
                Console.WriteLine("Please, enter the meter model ID.");
                Console.WriteLine("The available IDs are:");
                foreach (var id in MeterModelIdExtensions.GetMeterModelIds())
                {
                    Console.WriteLine($"{(int)id}) {id}");
                }
                meterModelId = CheckInt();
            } while (!MeterModelIdExtensions.IsMeterModelIdValid(meterModelId));

            return (EMeterModelId)meterModelId;
        }

        private int ReadMeterNumber()
        {
            Console.WriteLine("Please, enter the meter number.");
            int meterNumber = CheckInt();
            return meterNumber;
        }

        private string ReadMeterFirmwareVersion()
        {
            Console.WriteLine("Please, enter the meter firmware version.");
            string meterFirmwareVersion = CheckString();
            return meterFirmwareVersion;
        }

        private ESwitchState ReadSwitchState()
        {
            int switchState;

            do
            {
                Console.WriteLine("Enter the meter switch state.");
                Console.WriteLine("The available states are:");
                foreach (var state in SwitchStateExtensions.GetSwitchStates())
                {
                    Console.WriteLine($"{(int)state}) {state}");
                }
                switchState = CheckInt();
            } while (!SwitchStateExtensions.IsSwitchStateValid(switchState));

            return (ESwitchState) switchState;
        }
    }
}