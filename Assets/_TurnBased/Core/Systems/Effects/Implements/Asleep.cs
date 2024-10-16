using System;
using Sirenix.OdinInspector;
using TurnBasedPractice.Character;

namespace TurnBasedPractice.Effects
{
    [Serializable]
    public class Asleep : AbstractEffect
    {
        [ShowInInspector, ReadOnly]
        public override EffectName  Name  => EffectName.Asleep;
        public override EffectType  Type  => EffectType.Debuff;
        public override EffectOrder Order => EffectOrder.PreProcess;

        public override bool IsOvertime => false;

        public Asleep(){
            baseDuration  = 5;
            baseSuccessRate = 25;
            Duration = baseDuration;
        }

        public override void Effect(Hero owner){
            owner.IsAsleep = true;
            owner.IsConstraint = true;
            base.Effect(owner);
        }

        public override void Lift(Hero owner){
            owner.IsAsleep = false;
            base.Lift(owner);
        }
    }
}