using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands
{
    public class CreateCommand : BaseCommand
    {
        public override EEndpointCommands Id => EEndpointCommands.CreateCommand;

        public override string Description => "Insert a new endpoint";

        public string Command()
        {
            Console.WriteLine("Please, enter the serial number.");
            string endpointSerialNumber = CheckString();

            Console.WriteLine("Please, enter the meter model ID.");
            int meterModelId = CheckInt();

            Console.WriteLine("Please, enter the meter number.");
            int meterNumber = CheckInt();

            Console.WriteLine("Please, enter the meter firmware version.");
            string meterFirmwareVersion = CheckString();

            Console.WriteLine("Please, enter the switch state.");
            int switchState = CheckInt();
        }
    }
}