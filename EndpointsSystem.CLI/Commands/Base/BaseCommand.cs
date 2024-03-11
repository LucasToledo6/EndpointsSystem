namespace EndpointsSystem.CLI.Commands.Base
{
    public abstract class BaseCommand
    {
        public abstract int Id { get; }
        public abstract string Description { get; }

        public string CheckString()
        {
            var input = Console.ReadLine() ?? "";
            return input;
        }

        public int CheckInt()
        {
            var input = Console.ReadLine() ?? "";
            var hasParsed = int.TryParse(input, out var parsed);
            if (!hasParsed)
            {
                Console.WriteLine("Please, input a valid number.");
                return CheckInt();
            }

            return parsed;
        }
    }
}