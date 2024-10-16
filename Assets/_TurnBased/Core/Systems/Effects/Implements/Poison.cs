using System;
using Sirenix.OdinInspector;
using TurnBasedPractice.Character;
using TurnBasedPractice.InfoSystem;
using UnityEngine;

namespace TurnBasedPractice.Effects
{
    [Serializable]
    public class Poison : AbstractEffect
    {
        private float baseMultiplier;

        [ShowInInspector, ReadOnly]
        public override EffectName  Name  => EffectName.Poison;
        public override EffectType  Type  => EffectType.Debuff;
        public override EffectOrder Order => EffectOrder.PostProcess;

        public override bool IsOvertime => true;

        public Poison(){
            baseDuration = 4;
            baseSuccessRate = 30;
            baseMultiplier = 0.05f;
            Duration = baseDuration;
        }

        public override bool Bestow(Hero user, Hero target/* , Func<float> probability */)
        {
            var value            = UnityEngine.Random.Range(0f, 100f);
            var impactFactor     = user.Stats[StatsSystem.StatsName.Lv] - target.Stats[StatsSystem.StatsName.Lv];
            var successThreshold = Mathf.Max(baseDuration + impactFactor, 0);
            return value <= successThreshold;
        }
        
        public override bool Bestow(Hero user, Hero target, Factor thresholdFactor){
            return Bestow(user, target);
        }

        public override void Effect(Hero owner)
        {
            Duration--;
            var currentHp = owner.Stats[StatsSystem.StatsName.CurrentHp];
            var poisonDamage = Mathf.FloorToInt(owner.Stats[StatsSystem.StatsName.MaxHp] * baseMultiplier);
            var actualDamage = currentHp <= poisonDamage? currentHp-1 : poisonDamage;
            owner.TakeDamage(actualDamage);
            owner.OnHurt(null);

            BattleInfoSystem.Add(string.Format(effectTemplate, actualDamage));
        }
    }
}