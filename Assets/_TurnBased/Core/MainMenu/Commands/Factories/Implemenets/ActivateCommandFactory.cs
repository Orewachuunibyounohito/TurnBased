using UnityEngine;

namespace TurnBasedPractice.MainMenu.Commands.Factories
{
    public class ActivateCommandFactory : ICommandFactory
    {
        public ICommand Generate(GameObject target) => target.AddComponent<ActivateCommand>();
        public ICommand Generate(CommandScriptable commandScriptable) => commandScriptable.Target.AddComponent<ActivateCommand>();
    }
}