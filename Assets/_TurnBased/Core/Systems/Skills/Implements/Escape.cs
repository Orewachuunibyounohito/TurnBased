using TurnBasedPractice.Character;
using TurnBasedPractice.StatsSystem;
using UnityEngine;

namespace TurnBasedPractice.SkillSystem
{
    public class Escape : Skill
    {
        public override SkillName skillName => SkillName.Escape;
        public bool IsEscaped{ get; private set; } = true;

        public Escape() : base(){
            // Name = "Escape";
            Description = "Escape";
            ExecuteTime = 0.01f;
        }

        public override void Do(Hero hero, params Hero[] targets){
            float factor = Mathf.Max(hero.Stats[StatsName.Lv] - targets[0].Stats[StatsName.Lv], 0);
            IsEscaped = IsSucceed(factor);
        }

        private bool IsSucceed(float factor){
            float value   = Random.Range(0f, 100f);
            float success = 10 + factor;
            return value <= success;
        }
    }
}