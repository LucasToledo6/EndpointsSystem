using ANSIConsole;
using EndpointsSystem.CLI.Commands.Enums;
using System.Net.Http.Headers;

namespace EndpointsSystem.CLI.Commands.Base
{
    public abstract class BaseCommand
    {
        public abstract EEndpointCommands Id { get; }
        public abstract string Description { get; }
        protected HttpClient _client;

        public BaseCommand()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // Abstract method for executing each command
        public abstract Task ExecuteCommand();

        // Validates string user input
        public string CheckString()
        {
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Please, enter a valid input:");
                return CheckString();
            }

            return input;
        }

        // Validates int user input
        public int CheckInt()
        {
            string input = Console.ReadLine();
            bool hasParsed = int.TryParse(input, out var parsed);

            if (!hasParsed)
            {
                Console.WriteLine("Please, enter a valid input:");
                return CheckInt();
            }

            return parsed;
        }

        protected string ReadEndpointSerialNumber()
        {
            Console.WriteLine("Please, enter the " + "serial number:".Color("Blue").Bold());
            string endpointSerialNumber = CheckString();
            Console.WriteLine();
            return endpointSerialNumber;
        }
    }
}