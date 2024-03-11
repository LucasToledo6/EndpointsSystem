using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;
using System.Reflection;

namespace EndpointsSystem.CLI
{
    public class CommandOptions
    {
        public HashSet<BaseCommand> InitCommands()
        {
            var commandsTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(BaseCommand)));

            HashSet<BaseCommand> baseCommands = new HashSet<BaseCommand>();
            foreach (var commandType in commandsTypes)
            {
                var command = Activator.CreateInstance(commandType) as BaseCommand;
                baseCommands.Add(command!);
            }

            return baseCommands;
        }

        public void ListAllCommands(HashSet<BaseCommand> commands)
        {
            foreach (var command in commands.OrderBy(x => x.Id))
            {
                Console.WriteLine($"{command.Id}) {command.Description}");
            }
        }

        public EEndpointCommands? ReadInput()
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                if (Enum.IsDefined(typeof(EEndpointCommands), result))
                {
                    return (EEndpointCommands)result;
                }
            }
            return null;
        }
    }
}