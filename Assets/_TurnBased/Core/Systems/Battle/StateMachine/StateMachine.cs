using UnityEngine;

namespace TurnBasedPractice.BattleCore
{
    public class StateMachine
    {
        public IState CurrentState{ get; set; }

        public void Initialize(IState initState){
            CurrentState = initState;
            CurrentState.Enter();
        }
        public void ChangeState(IState nextState){
            // Debug.Log($"{CurrentState} -> {nextState}");
            CurrentState.Exit();
            CurrentState = nextState;
            CurrentState.Enter();
        }
    }
}
