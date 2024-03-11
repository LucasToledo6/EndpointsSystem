﻿using EndpointsSystem.CLI.Commands.Base;
using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands
{
    public class FindCommand : BaseCommand
    {
        public override int Id => (int)EEndpointCommands.FindCommand;

        public override string Description => "Find a endpoint by serial number";
    }
}