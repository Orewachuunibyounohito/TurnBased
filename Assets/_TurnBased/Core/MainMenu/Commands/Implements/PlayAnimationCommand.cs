using Sirenix.OdinInspector;
using UnityEngine;

namespace TurnBasedPractice.MainMenu.Commands
{
    public class PlayAnimationCommand : MonoBehaviour, IAnimateCommand
    {
        [ReadOnly]
        public string AnimationName{ get; set; }
        public float Duration;
        private bool _isRunning;
        private Animator _animator;
        private Animator animator{ 
            get{
                if(_animator == null){
                    _animator = GetComponent<Animator>();
                }
                return _animator;
            }
            set => _animator = value;
        }

        public bool IsRunning{ 
            get => _isRunning;
            private set => _isRunning = value;
        }

        public void Execute(){
            IsRunning = true;
            animator.enabled = true;
            animator.Play(AnimationName);
            IsRunning = false;
        }
        public void Finish(){
            IsRunning = false;
        }
        public void Skip(){
            animator.Play(AnimationName, 0, 0.99f);
            Finish();
        }
    }
}