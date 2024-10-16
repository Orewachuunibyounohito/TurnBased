using System;
using Sirenix.OdinInspector;
using TurnBasedPractice.MainMenu.Commands;

public partial class MainMenuFlow
{
    [Serializable]
    class AnimateCommandFields
    {
        [ShowInInspector]
        public IAnimateCommand AnimateCommand;
        public bool PlayImmediately;
        public CustomEndTime CustomEndTime;

        public bool IsRunning => AnimateCommand.IsRunning;
        public void Execute() => AnimateCommand?.Execute();
        public void Finish() => AnimateCommand?.Finish();
        public void Skip() => AnimateCommand?.Skip();
    }
}

