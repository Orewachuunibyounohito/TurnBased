namespace TurnBasedPractice.MainMenu.Commands
{
    public interface IAnimateCommand : ICommand
    {
        bool IsRunning { get; }
        void Finish();
        void Skip();
    }
}