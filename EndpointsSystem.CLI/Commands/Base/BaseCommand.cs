using EndpointsSystem.CLI.Commands.Enums;
using System.Net.Http.Headers;

namespace EndpointsSystem.CLI.Commands.Base
{
    public abstract class BaseCommand
    {
        protected HttpClient _client;
        public abstract EEndpointCommands Id { get; }
        public abstract string Description { get; }

        public BaseCommand()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public abstract Task ExecuteCommand();

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

        protected string ReadEndpointSerialNumber()
        {
            Console.WriteLine("Please, enter the serial number.");
            string endpointSerialNumber = CheckString();
            return endpointSerialNumber;
        }

        protected string ReadConfirmation()
        {
            Console.WriteLine("Are you sure you want to delete this endpoint? (y/n)");
            string confirmation = CheckString();
            return confirmation;
        }
    }
}