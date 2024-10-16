using UnityEngine;

namespace TurnBasedPractice.MainMenu.Commands
{
    public class ActivateCommand : MonoBehaviour, IAnimateCommand
    {
        public bool IsRunning{ get; private set; }

        public void Execute(){
            IsRunning = true;
            gameObject.SetActive(true);
            IsRunning = false;
        }
        public void Finish() => IsRunning = false;
        public void Skip(){
            Finish();
        }
    }
}