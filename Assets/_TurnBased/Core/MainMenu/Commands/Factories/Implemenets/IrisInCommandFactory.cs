using TurnBasedPractice.Animates;
using UnityEngine;

namespace TurnBasedPractice.MainMenu.Commands.Factories
{
    public class IrisInCommandFactory : ICommandFactory
    {
        public ICommand Generate(GameObject target) => target.AddComponent<IrisIn>();
        public ICommand Generate(CommandScriptable commandScriptable){
            IrisIn irisIn = commandScriptable.Target.AddComponent<IrisIn>();
            irisIn = commandScriptable.ContentPasteTo(irisIn);
            return irisIn;
        }
    }
}