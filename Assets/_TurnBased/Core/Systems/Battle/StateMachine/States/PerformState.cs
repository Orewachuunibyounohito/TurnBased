using System.Collections.Generic;
using TurnBasedPractice.BattleCore.Loggers;
using TurnBasedPractice.Character;
using UnityEngine;

namespace TurnBasedPractice.BattleCore
{
    public class PerformState : BattleState
    {
        private Queue<Hero> playerQueue = new Queue<Hero>();
        private Hero currentPlayer;

        public bool IsBattleOver{ get; set; } = false;

        private const BattleStateType SELECT = BattleStateType.Select; 
        private const BattleStateType END    = BattleStateType.End; 

        public PerformState(BattleSystem battleSystem, StateMachine stateMachine) : base(battleSystem, stateMachine){
            stateType = BattleStateType.Perform;
        }

        public override void Enter(){
            base.Enter();

            _battleSystem.player1.IsReady = false;
            _battleSystem.player2.IsReady = false;

            playerQueue = ArrangePerformOrder();
            
            NextPlayerPerform();
        }

        private Queue<Hero> ArrangePerformOrder(){
            Queue<Hero> playerQueue = new Queue<Hero>();
            var p1FinalPriority = _battleSystem.player1.ActionPriority * Random.Range( 0.9f, 1.1f );
            var p2FinalPriority = _battleSystem.player2.ActionPriority * Random.Range( 0.9f, 1.1f );
            if(p1FinalPriority >= p2FinalPriority){
                playerQueue.Enqueue(_battleSystem.player1);
                playerQueue.Enqueue(_battleSystem.player2);
            }else{
                playerQueue.Enqueue(_battleSystem.player2);
                playerQueue.Enqueue(_battleSystem.player1);
            }
            return playerQueue;
        }

        private void NextPlayerPerform(){
            currentPlayer = playerQueue.Dequeue();
            currentPlayer.Performing(IsBattleOver);
        }

        public override void FrameUpdate(){
            if(IsPerforming()){ return ; }
            Performing();

            bool isReadyToChangeStage = _battleSystem.player1.IsReady && _battleSystem.player2.IsReady;
            if (isReadyToChangeStage) { ChangeStage(); }
        }

        private bool IsPerforming(){
            return !currentPlayer.Controller.GetSelected().IsFinish;
        }

        private void Performing(){
            if (playerQueue.Count == 0){ return ; }
            if (currentPlayer.IsReady){
                NextPlayerPerform();
            }
        }

        private void ChangeStage(){
            if (IsBattleOver){
                _stateMachine.ChangeState(_battleSystem.BattleStates[END]);
            }else{
                _stateMachine.ChangeState(_battleSystem.BattleStates[SELECT]);
            }
        }

        public override void PhysicsUpdate(){
        }

        public override void Exit(){
            base.Exit();

            PlayerReset(_battleSystem.player1);
            PlayerReset(_battleSystem.player2);

            currentPlayer = default;

            if(_battleSystem.BattleUI.SkillPanel.gameObject.activeInHierarchy){
                _battleSystem.BattleUI.SkillPanel.gameObject.SetActive(false);
            }

            _battleSystem.BattleUI.InfoText.SetText("");
        }

        private void PlayerReset(Hero player){
            player.IsReady = false;
            if(player.Stats.ContainsKey(StatsSystem.StatsName.Guard)){
                player.Stats.Remove(StatsSystem.StatsName.Guard);
            }
        }
    }
}
