using TurnBasedPractice.Animates;
using UnityEngine;

namespace TurnBasedPractice.MainMenu.Commands.Factories
{
    public class IrisInUiCommandFactory : ICommandFactory
    {
        public ICommand Generate(GameObject target) => target.AddComponent<IrisIn_UI>();

        public ICommand Generate(CommandScriptable commandScriptable){
            IrisIn irisInUi = commandScriptable.Target.AddComponent<IrisIn_UI>();
            irisInUi = commandScriptable.ContentPasteTo(irisInUi);
            return irisInUi;
        }
    }
}