using System;
using System.Collections.Generic;
using TurnBasedPractice.Animates;
using UnityEngine;

namespace TurnBasedPractice.MainMenu.Commands.Factories
{
    public class CommandFactories
    {
        private static Dictionary<CommandType, ICommandFactory> factories;

        static CommandFactories(){
            factories = new Dictionary<CommandType, ICommandFactory>(){
                { CommandType.Activate, new ActivateCommandFactory() },
                { CommandType.FadeIn, new FadeInCommandFactory() },
                { CommandType.IrisIn, new IrisInCommandFactory() },
                { CommandType.IrisInUi, new IrisInUiCommandFactory() },
                { CommandType.PlayAnimation, new PlayAnimationCommandFactory() },
                { CommandType.Move, new MoveLoopWithBoundsAnimateFactory() }
            };
        }

        public static ICommand Generate(CommandType commandType, GameObject target) => factories[commandType].Generate(target);
        public static ICommand Generate(CommandScriptable commandScriptable) => factories[commandScriptable.Type].Generate(commandScriptable);
    }

    public class MoveLoopWithBoundsAnimateFactory : ICommandFactory
    {
        public ICommand Generate(GameObject target) => throw new NotImplementedException();
        public ICommand Generate(CommandScriptable commandScriptable){
            MoveLoopWithBoundsAnimate moveAnimate = commandScriptable.Target.AddComponent<MoveLoopWithBoundsAnimate>();
            moveAnimate = commandScriptable.ContentPasteTo(moveAnimate);
            return moveAnimate;
        }
    }

    public class FadeInCommandFactory : ICommandFactory
    {
        public ICommand Generate(GameObject target) => throw new NotImplementedException();
        public ICommand Generate(CommandScriptable commandScriptable) => throw new NotImplementedException();
    }
}