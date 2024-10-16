using System;
using System.Collections;
using TurnBasedPractice.Character;
using TurnBasedPractice.InfoSystem;
using UnityEngine;

namespace TurnBasedPractice.BattleCore.Selection
{
    public class EscapedAction : ICustomAction
    {
        private bool _isFinish = false;

        public Hero User { get; set; }
        public Hero[] Targets { get; set; }
        public float ExecuteTime { get; set; } = Time.deltaTime;
        public string ExecuteInfo { get; set; }

        public bool IsFinish => _isFinish;

        public EscapedAction(Hero user){
            User = user;
        }

        public void DoAction(MonoBehaviour mono){
            Finish();
            User.PhaseOK();
        }

        public void Finish() => _isFinish = true;
        public void Reset()  => _isFinish = false;
    }
}
