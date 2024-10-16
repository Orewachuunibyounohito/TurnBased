using TurnBasedPractice.BattleCore.Loggers;

namespace TurnBasedPractice.BattleCore
{
    public class StartState : BattleState
    {
        private const BattleStateType SELECT = BattleStateType.Select;

        public StartState(BattleSystem battleSystem, StateMachine stateMachine) : base(battleSystem, stateMachine){
            stateType = BattleStateType.Start;
        }

        public override void Enter(){
            base.Enter();
            _battleSystem.player1.PhaseOK();
            _battleSystem.player2.PhaseOK();
        }

        public override void FrameUpdate(){
            if(_battleSystem.player1.IsReady && _battleSystem.player2.IsReady){
                _stateMachine.ChangeState(_battleSystem.BattleStates[SELECT]);
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