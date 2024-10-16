using System;
using TurnBasedPractice.Character;
using UnityEngine;

namespace TurnBasedPractice.BattleCore.Selection
{
    public interface ICustomAction
    {
        public Hero   User{ get; set; }
        public Hero[] Targets{ get; set; }
        public float  ExecuteTime{ get; set; }
        public string ExecuteInfo{ get; set; }
        public bool   IsFinish{ get; }

        public void DoAction(MonoBehaviour mono);
        public void Finish();
        public void Reset();
    }
}