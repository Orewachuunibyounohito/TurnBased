using System;
using TurnBasedPractice.BattleCore.Loggers;

namespace TurnBasedPractice.BattleCore
{
    [Serializable]
    public class BattleState : IState
    {
        protected BattleSystem _battleSystem;
        protected StateMachine _stateMachine;

        protected BattleStateType stateType;

        public BattleState(BattleSystem battleSystem, StateMachine stateMachine){
            _battleSystem = battleSystem;
            _stateMachine = stateMachine;
        }

        public virtual void Enter() => LoggerSystem.EnterForBattleState(stateType);
        public virtual void FrameUpdate(){}
        public virtual void PhysicsUpdate(){}
        public virtual void Exit() => LoggerSystem.ExitForBattleState(stateType);
    }
}