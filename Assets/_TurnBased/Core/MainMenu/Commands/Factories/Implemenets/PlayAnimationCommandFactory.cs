using TurnBasedPractice.Animates;
using UnityEngine;

namespace TurnBasedPractice.MainMenu.Commands.Factories
{
    public class PlayAnimationCommandFactory : ICommandFactory
    {
        public ICommand Generate(GameObject target) => throw new System.NotImplementedException();

        public ICommand Generate(CommandScriptable commandScriptable){
            PlayAnimationCommand command = commandScriptable.Target.AddComponent<PlayAnimationCommand>();
            command = commandScriptable.ContentPasteTo(command);
            return command;
        }
    }
}