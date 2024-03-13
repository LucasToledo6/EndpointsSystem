using ANSIConsole;
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

        public override string Description => "Insert".Underlined() + " a new endpoint";

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

            // Focused on trying to give user friendly exception messages
            if (!createEndpointResponse.IsSuccessStatusCode)
            {
                var errorContent = await createEndpointResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"{errorContent}".Color("Red"));
                return;
            }

            Console.WriteLine("Endpoint created successfully.".Color("Lightgreen"));
        }

        private EMeterModelId ReadMeterModelId()
        {
            int meterModelId;

            do
            {
                Console.WriteLine("Please, enter the " + "meter model ID:".Color("Blue").Bold());
                Console.WriteLine("The " + "available IDs".Color("Blue").Bold() + " are:");

                // Using a Extension class to list and validates all Meter Model IDs
                foreach (var id in MeterModelIdExtensions.GetMeterModelIds())
                {
                    Console.WriteLine($"{(int)id})".Color("Blue").Bold() + $" {id}");
                }

                meterModelId = CheckInt();

            } while (!MeterModelIdExtensions.IsMeterModelIdValid(meterModelId));

            Console.WriteLine();

            return (EMeterModelId)meterModelId;
        }

        private int ReadMeterNumber()
        {
            Console.WriteLine("Please, enter the " + "meter number:".Color("Blue").Bold());
            int meterNumber = CheckInt();
            Console.WriteLine();
            return meterNumber;
        }

        private string ReadMeterFirmwareVersion()
        {
            Console.WriteLine("Please, enter the " + "meter firmware version:".Color("Blue").Bold());
            string meterFirmwareVersion = CheckString();
            Console.WriteLine();
            return meterFirmwareVersion;
        }

        private ESwitchState ReadSwitchState()
        {
            int switchState;

            do
            {
                Console.WriteLine("Please, enter the " + "meter switch state:".Color("Blue").Bold());
                Console.WriteLine("The " + "available switch states".Color("Blue").Bold() + " are:");

                // Same principle as the Meter Model IDs
                foreach (var state in SwitchStateExtensions.GetSwitchStates())
                {
                    Console.WriteLine($"{(int)state})".Color("Blue").Bold() + $" {state}");
                }

                switchState = CheckInt();

            } while (!SwitchStateExtensions.IsSwitchStateValid(switchState));

            Console.WriteLine();

            return (ESwitchState) switchState;
        }
    }
}