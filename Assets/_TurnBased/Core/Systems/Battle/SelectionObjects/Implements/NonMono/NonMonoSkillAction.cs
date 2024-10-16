using System;
using Sirenix.OdinInspector;
using TurnBasedPractice.Character;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedPractice.BattleCore.Selection
{
    [Serializable]
    public class NonMonoSkillAction : NonMonoButtonAction
    {
        [ShowInInspector]
        public Skill skill{ get; set; }

        public bool CanUse => skill.CanUse(User);

        public NonMonoSkillAction(Button button, Skill skill, Hero user, params Hero[] targets) : base(button, user, targets) =>
            this.skill = skill;

        public override void DoAction(MonoBehaviour mono){
            skill.Do(User, Targets);
            base.DoAction(mono);
        }

        public void SetSkill(Skill skill){
            this.skill  = skill;
            ExecuteTime = skill.ExecuteTime;
        }
    }
}
