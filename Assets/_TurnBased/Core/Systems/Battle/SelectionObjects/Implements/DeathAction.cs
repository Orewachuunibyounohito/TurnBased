using System;
using System.Collections;
using TurnBasedPractice.Character;
using TurnBasedPractice.InfoSystem;
using TurnBasedPractice.Localization;
using UnityEngine;

namespace TurnBasedPractice.BattleCore.Selection
{
    public class DeathAction : ICustomAction
    {
        private bool _isFinish = false;

        public Hero User { get; set; }
        public Hero[] Targets { get; set; }
        public float ExecuteTime { get; set; } = Time.deltaTime;
        public string ExecuteInfo { get; set; }

        private string defeatTemplate => LocalizationSettings.GetCommonString(CommonStringTemplate.Defeat).GetLocalizedString();

        public bool IsFinish => _isFinish;

        public DeathAction(Hero user, string attacker){
            User = user;
            ExecuteInfo = string.Format(defeatTemplate, User.Name, attacker);
        }

        public void DoAction(MonoBehaviour mono){
            BattleInfoSystem.Clear();
            BattleInfoSystem.Add(ExecuteInfo);
            BattleInfoSystem.Play();

            mono.StartCoroutine(ExecuteTask());
        }

        public void Finish() => _isFinish = true;
        public void Reset()  => _isFinish = false;

        private IEnumerator ExecuteTask(){
            yield return new WaitForSeconds(ExecuteTime);
            while(!BattleInfoSystem.IsFinish){
                yield return null;
            }
            Finish();
            User.PhaseOK();
        }
    }
}
