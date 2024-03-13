using EndpointsSystem.CLI;

CommandOptions commandOptions = new();
var commands = commandOptions.InitCommands();

while (true)
{
    commandOptions.ListAllCommands(commands);
    var input = commandOptions.ReadInput();
    if (input == null)
    {
        Console.WriteLine("The input was not recognized as a valid command.");
        PromptToContinue();
        continue;
    }

    await commandOptions.ExecuteCommand(commands, input);
    PromptToContinue();
}

static void PromptToContinue()
{
    Console.WriteLine("Press any key to continue.");
    Console.ReadKey();
    Console.Clear();
}