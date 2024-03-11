using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;
using EndpointsSystem.CLI.Commands.Extensions;

namespace EndpointsSystem.CLI.Commands
{
    public class CreateCommand : BaseCommand
    {
        public override EEndpointCommands Id => EEndpointCommands.CreateCommand;

        public override string Description => "Insert a new endpoint";

        public void CreateCommandBody()
        {
            ReadEndpointSerialNumber();
            ReadMeterModelId();
            ReadMeterNumber();
            ReadMeterFirmwareVersion();
            ReadSwitchState();
        }

        private string ReadEndpointSerialNumber()
        {
            Console.WriteLine("Please, enter the serial number.");
            string endpointSerialNumber = CheckString();
            return endpointSerialNumber;
        }

        private int ReadMeterModelId()
        {
            Console.WriteLine("Please, enter the meter model ID.");
            int meterModelId = CheckInt();
            return meterModelId;
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

        private int ReadSwitchState()
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

            return switchState;
        }
    }
}