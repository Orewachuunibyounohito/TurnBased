using TurnBasedPractice.Character.Controller;

namespace TurnBasedPractice.BattleCore.Selection
{
    public interface IFocusable
    {
        public void Interact(PlayerController playerController);
        public void Interact();
        public void OnFocusEnter();
        public void OnFocusClick();
        public void OnFocusExit();
    }
}