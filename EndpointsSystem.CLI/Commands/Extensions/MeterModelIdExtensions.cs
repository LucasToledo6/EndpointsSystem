using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands.Extensions
{
    public class MeterModelIdExtensions
    {
        // List all available meter model IDs options
        public static List<EMeterModelId> GetMeterModelIds()
        {
            return Enum.GetValues<EMeterModelId>().ToList();
        }

        // Validates Meter Model ID
        public static bool IsMeterModelIdValid(int input)
        {
            return Enum.IsDefined(typeof(EMeterModelId), input);
        }
    }
}
