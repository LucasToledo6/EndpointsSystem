using EndpointsSystem.CLI;

CommandOptions commandOptions = new CommandOptions();
var commands = commandOptions.InitCommands();

while (true)
{
    commandOptions.ListAllCommands(commands);
    int input = int.Parse(Console.ReadLine());
    Console.ReadKey();
}