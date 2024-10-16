using TurnBasedPractice.BattleCore.Loggers;
using TurnBasedPractice.Character;
using TurnBasedPractice.Character.Controller;

namespace TurnBasedPractice.BattleCore
{
    public class SelectState : BattleState
    {
        private const BattleStateType PERFORM = BattleStateType.Perform;

        public SelectState(BattleSystem battleSystem, StateMachine stateMachine) : base(battleSystem, stateMachine){
            stateType = BattleStateType.Select;
        }

        public override void Enter(){
            base.Enter();
            var p1 = (PlayerController)_battleSystem.player1.Controller;
            p1.stateMachine.ChangeState(p1.ControlStates[ControlStateType.Normal]);
        }

        public override void FrameUpdate(){
            Selecting(_battleSystem.player1);
            Selecting(_battleSystem.player2);
            HandleToNextState();
        }

        private void Selecting(Hero player){
            if (!player.IsReady){
                player.Controller.Selecting();
            }
        }

        private void HandleToNextState(){
            bool isReadyToPerformState = _battleSystem.player1.IsReady && _battleSystem.player2.IsReady;
            if (isReadyToPerformState){
                _stateMachine.ChangeState(_battleSystem.BattleStates[PERFORM]);
            }
        }

        public override void PhysicsUpdate(){
        }

        public override void Exit(){
            base.Exit();
            _battleSystem.player1.IsReady = false;
            _battleSystem.player2.IsReady = false;
        }
    }
}
