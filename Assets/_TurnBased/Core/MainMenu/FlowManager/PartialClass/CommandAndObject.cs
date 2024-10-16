using System;
using Sirenix.OdinInspector;
using TurnBasedPractice.MainMenu.Commands;
using UnityEngine;

public partial class MainMenuFlow
{
    [Serializable]
    class CommandAndObject
    {
        public static implicit operator CommandScriptable(CommandAndObject commandAndObject) => commandAndObject.Command;
        
        [InlineEditor]
        public CommandScriptable Command;
        public GameObject Target;

        public bool PlayImmediately => Command.PlayImmediately;
    }
}

