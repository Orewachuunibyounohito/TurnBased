using System;
using TurnBasedPractice.Character;
using TurnBasedPractice.Localization;
using TurnBasedPractice.StatsSystem;
using TurnBasedPractice.StatsSystem.Exception;
using UnityEngine;

namespace TurnBasedPractice.SkillSystem
{
    [Serializable]
    public class Impact : Skill
    {
        public override SkillName skillName{ get => SkillName.Impact; }
        
        public Impact() : base(){ Init(); }

        public override void Do(Hero user, params Hero[] targets)
        {
            user.Controller.GetSelected().ExecuteInfo = UseText(user);

            int damage;
            foreach (var target in targets)
            {
                damage = CalculateDamage(user, target);
                damage = HandleGuard(damage, target);
                target.TakeDamage(damage);

                user.Controller.GetSelected().ExecuteInfo += "\n" + AttackText(target, damage);

                target.OnHurt(user);
            }
            base.Do(user, targets);
        }

        private int CalculateDamage(Hero user, Hero target){
            int damage = 0;
            try{
                damage = Math.Clamp(user.Stats[StatsName.Strength] - target.Stats[StatsName.Defense], 0, int.MaxValue);
            }catch(StatsElementNotFoundException e){
                Debug.Log($"{e}");
            }
            return damage;
        }

        private void Init(){
            // Name        = "Impact";
            Description = "A physics impact!";
            ExecuteTime = 1.2f;
        }
    }
}