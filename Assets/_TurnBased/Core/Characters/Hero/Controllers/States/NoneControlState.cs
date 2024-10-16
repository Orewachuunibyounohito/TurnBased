using TurnBasedPractice.BattleCore;
using TurnBasedPractice.BattleCore.Loggers;

namespace TurnBasedPractice.Character.Controller
{
    public class NoneControlState : IState
    {
        private readonly PlayerController _controller;

        public NoneControlState(PlayerController playerController){
            _controller = playerController;
        }

        public void Enter(){
            LoggerSystem.EnterForControlState(ControlStateType.None);
        }
        public void Exit(){
            LoggerSystem.ExitForControlState(ControlStateType.None);
            if(_controller.selectedAction != null){
                _controller.selectedAction.Reset();
            }
        }
        public void FrameUpdate(){}
        public void PhysicsUpdate(){}
    }
}
