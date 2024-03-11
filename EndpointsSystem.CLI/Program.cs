using EndpointsSystem.CLI;

CommandOptions commandOptions = new CommandOptions();
var commands = commandOptions.InitCommands();

while (true)
{
    commandOptions.ListAllCommands(commands);
    var input = commandOptions.ReadInput();
    if (input == null)
    {
        Console.WriteLine("The input was not recognized as a valid command.");
        Console.WriteLine("Press any key to continue.");
        Console.ReadKey();
        Console.Clear();
        continue;
    }
    Console.ReadKey();
    Console.Clear();
}