using System.Collections;
using TurnBasedPractice.BattleCore.Loggers;
using TurnBasedPractice.InfoSystem;
using TurnBasedPractice.Localization;
using UnityEditor;
using UnityEngine;

namespace TurnBasedPractice.BattleCore
{
    public class EndState : BattleState
    {
        private static string deathTemplate;
        private static string escapeTemplate;

        private bool         isPreparing = true;
        private BattleResult battleResult;

        static EndState(){
            LocalizationSettings.GetCommonString(CommonStringTemplate.Death).StringChanged += UpdateDeathTemplate;
            LocalizationSettings.GetCommonString(CommonStringTemplate.Escape).StringChanged += UpdateEscapeTemplate;
        }

        public EndState(BattleSystem battleSystem, StateMachine stateMachine) : base(battleSystem, stateMachine){
            stateType = BattleStateType.End;
        }

        public override void Enter()
        {
            base.Enter();
            battleResult.winner.PhaseOK();
            _battleSystem.StartCoroutine(EndingTask());
        }

        private IEnumerator EndingTask(){
            ReasonOfBattleEnd();
            while (!BattleInfoSystem.IsFinish){
                yield return null;
            }
            isPreparing = false;
            battleResult.loser.Controller.GetSelected().DoAction(_battleSystem);
        }

        private void ReasonOfBattleEnd(){
            BattleInfoSystem.Clear();

            switch(battleResult.reason){
                case VictoryReason.EnemyEscaped:
                    BattleInfoSystem.Add(EscapeText());
                    battleResult.loser.Controller.PlayerEscaped();
                    break;
                case VictoryReason.EnemyDefeat:
                    BattleInfoSystem.Add(LoserText());
                    battleResult.loser.Controller.PlayerDie(battleResult.winner);
                    break;
                default: throw new System.Exception("Not ending action, checking condition of BattleEnd please!");
            }
            
            BattleInfoSystem.Play();
        }

        private string LoserText(){
            return string.Format(deathTemplate, battleResult.loser.Name);
        }
        private string EscapeText(){
            return string.Format(escapeTemplate, battleResult.loser.Name);
        }

        public override void FrameUpdate(){
            if(isPreparing){ return ; }
            
            bool isReadyToBattleEnd = _battleSystem.player1.IsReady && _battleSystem.player2.IsReady;
            if (isReadyToBattleEnd){ Exit(); }
        }

        public override void PhysicsUpdate(){
        }

        public override void Exit(){
            base.Exit();
            LoggerSystem.ShowLogForUnity();
            #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }

        public void SetBattleResult(BattleResult battleResult){
            this.battleResult = battleResult;
        }

        private static void UpdateDeathTemplate(string value) => deathTemplate = value;
        private static void UpdateEscapeTemplate(string value) => escapeTemplate = value;
    }
}
