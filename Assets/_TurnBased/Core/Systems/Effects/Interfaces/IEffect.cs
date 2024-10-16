using System;
using TurnBasedPractice.Character;

namespace TurnBasedPractice.Effects
{
    public interface IEffect
    {
        EffectName  Name { get; }
        EffectType  Type { get; }
        EffectOrder Order{ get; }
        bool        IsOvertime{ get; }
        int         Duration{ get; set; }

        bool Bestow(Hero user, Hero target);
        bool Bestow(Hero user, Hero target, Factor thresholdFactor);
        void Effect(Hero owner);
        void Lift(Hero owner);

        string BestowedText(Hero target);
        string LiftText(Hero owner);
    }

    public class Factor
    {
        public int value;
        public int percent;
        public float multiplier;

        public float Calculate(int baseValue){
            var addedValue = baseValue + value;
            var addedPercentValue = addedValue * (1 + percent/100f);
            var multipliedVaule = addedPercentValue * multiplier;
            return multipliedVaule;
        }
    }
}