using System;
using TurnBasedPractice.Character;
using TurnBasedPractice.StatsSystem;
using TurnBasedPractice.StatsSystem.Exception;
using UnityEngine;

namespace TurnBasedPractice.SkillSystem
{
    [Serializable]
    public class WaterBall : Skill
    {
        public override SkillName skillName{ get => SkillName.WaterBall; }

        public WaterBall() : base(){
            Init();
            ConsumedStats.Add(StatsName.CurrentMp, 5);
        }

        public override void Do(Hero user, params Hero[] targets){
            if(CanUse(user))
            {
                user.Controller.GetSelected().ExecuteInfo = UseText(user);

                int damage;
                foreach (var target in targets)
                {
                    damage = CalculateDamage(user);
                    damage = HandleGuard(damage, target);
                    target.TakeDamage(damage);

                    user.Controller.GetSelected().ExecuteInfo += "\n" + AttackText(target, damage);

                    target.OnHurt(user);
                }
                CalculateConsumption(user);
                base.Do(user, targets);
            }
        }

        private int CalculateDamage(Hero user){
            int damage = 0;
            try{
                damage = Math.Clamp(user.Stats[StatsName.Wisdom] * 2, 0, int.MaxValue);
            }catch(StatsElementNotFoundException e){
                Debug.Log($"{e}");
            }
            return damage;
        }
        private void CalculateConsumption(Hero user){
            foreach(var consumedData in ConsumedStats.Values){
                user.Stats[consumedData.Name] -= consumedData.Value;
            }
        }

        private void Init(){
            // Name        = "WaterBall";
            Description = "A magic of aqua type, look like a ball.";
            ExecuteTime = 2f;
        }
    }
}
