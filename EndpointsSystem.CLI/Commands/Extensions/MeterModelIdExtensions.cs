using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands.Extensions
{
    public class MeterModelIdExtensions
    {
        public static List<EMeterModelId> GetMeterModelIds()
        {
            return Enum.GetValues<EMeterModelId>().ToList();
        }

        public static bool IsMeterModelIdValid(int input)
        {
            return Enum.IsDefined(typeof(EMeterModelId), input);
        }
    }
}
