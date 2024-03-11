﻿using EndpointsSystem.CLI.Commands.Enums;

namespace EndpointsSystem.CLI.Commands.Extensions
{
    public static class SwitchStateExtensions
    {
        public static List<ESwitchState> GetSwitchStates()
        {
            return Enum.GetValues<ESwitchState>().ToList();
        }

        public static bool IsSwitchStateValid(int input)
        {
            return Enum.IsDefined(typeof(ESwitchState), input);
        }
    }
}
