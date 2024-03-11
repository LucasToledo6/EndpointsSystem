using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands.Base
{
    public abstract class BaseCommand
    {
        public abstract EEndpointCommands Id { get; }
        public abstract string Description { get; }

        public string CheckString()
        {
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Please, enter a valid input.");
                return CheckString();
            }

            return input;
        }

        public int CheckInt()
        {
            string input = Console.ReadLine();
            bool hasParsed = int.TryParse(input, out var parsed);

            if (!hasParsed)
            {
                Console.WriteLine("Please, enter a valid input.");
                return CheckInt();
            }

            return parsed;
        }
    }
}