using System;
using Sirenix.OdinInspector;
using TurnBasedPractice.Character;
using TurnBasedPractice.InfoSystem;
using TurnBasedPractice.Localization;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TurnBasedPractice.Effects
{
    [Serializable]
    public class Paralysis : AbstractEffect
    {
        private int activateRate = 50;

        [ShowInInspector, ReadOnly]
        public override EffectName  Name  => EffectName.Paralysis;
        public override EffectType  Type  => EffectType.Debuff;
        public override EffectOrder Order => EffectOrder.PreProcess;

        public override bool IsOvertime => false;

        public Paralysis(){
            baseDuration = 3;
            baseSuccessRate = 25;
            activateRate = 50;
            Duration = baseDuration;
        }

        public override bool Bestow(Hero user, Hero target, Factor thresholdFactor){
            return Bestow(user, target);
        }

        public override void Effect(Hero owner){
            Duration--;
            if(Activate()){
                owner.IsConstraint = true;
                BattleInfoSystem.Add(string.Format(effectTemplate, owner.Name));
            }
        }

        private bool Activate() => Random.Range(0f, 100f) <= activateRate;
    }
}