using EndpointsSystem.CLI.Commands.Base;
using System.Reflection;

namespace EndpointsSystem.CLI
{
    public class CommandOptions
    {
        public List<BaseCommand> InitCommands()
        {
            var commandsTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => !x.IsAbstract && x.IsSubclassOf(typeof(BaseCommand)));

            List<BaseCommand> baseCommands = new List<BaseCommand>();
            foreach (var commandType in commandsTypes)
            {
                var command = Activator.CreateInstance(commandType) as BaseCommand;
                baseCommands.Add(command!);
            }

            return baseCommands;
        }

        public void ListAllCommands(List<BaseCommand> commands)
        {
            foreach (var command in commands.OrderBy(x => x.Id))
            {
                Console.WriteLine($"{command.Id}) {command.Description}");
            }
        }

    }
}