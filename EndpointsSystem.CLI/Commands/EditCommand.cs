using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands
{
    public class EditCommand : BaseCommand
    {
        public override int Id => (int)EEndpointCommands.EditCommand;

        public override string Description => "Edit an existing endpoint";

        public string Command()
        {
            Console.WriteLine("Please, enter the endpoint serial number.");
            int endPointSerialNumber = CheckInt();

            Console.WriteLine("Please, enter the endpoint's new switch state.");
            int newSwitchState = CheckInt();
        }
    }
}