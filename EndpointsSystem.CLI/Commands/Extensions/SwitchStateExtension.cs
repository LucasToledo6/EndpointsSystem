using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands.Extensions
{
    public static class SwitchStateExtensions
    {
        // List all available Switch State options
        public static List<ESwitchState> GetSwitchStates()
        {
            return Enum.GetValues<ESwitchState>().ToList();
        }

        // Validates Switch State
        public static bool IsSwitchStateValid(int input)
        {
            return Enum.IsDefined(typeof(ESwitchState), input);
        }
    }
}
