using UnityEngine;

namespace TurnBasedPractice.MainMenu.Commands.Factories
{
    public interface ICommandFactory
    {
        ICommand Generate(GameObject target);
        ICommand Generate(CommandScriptable commandScriptable);
    }
}