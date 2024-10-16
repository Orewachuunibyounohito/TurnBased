using UnityEngine;

namespace TurnBasedPractice.BattleCore
{
    public class OtherState : BattleState
    {
        public OtherState(BattleSystem battleSystem, StateMachine stateMachine) : base(battleSystem, stateMachine){}

        public override void Enter(){
            Debug.Log($"Other Phase!");
        }

        public override void FrameUpdate(){
            if(_battleSystem.player1.IsReady && _battleSystem.player2.IsReady){}
        }

        public override void PhysicsUpdate(){
        }

        public override void Exit(){
            _battleSystem.player1.IsReady = false;
            _battleSystem.player2.IsReady = false;
        }
    }
}
