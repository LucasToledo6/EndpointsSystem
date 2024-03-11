namespace EndpointsSystem.CLI.Commands.Base
{
    public abstract class BaseCommand
    {
        public abstract int Id { get; }
        public abstract string Description { get; }
    }
}