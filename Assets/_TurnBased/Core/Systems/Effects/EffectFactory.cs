using System;

namespace TurnBasedPractice.Effects
{
    public class EffectFactory
    {
        public static IEffect Generate(EffectName effectName) => effectName switch{
            EffectName.Poison => new Poison(),
            EffectName.Paralysis => new Paralysis(),
            EffectName.Asleep => new Asleep(),
            _ => throw new ArgumentException("Unrecognized Effect!")
        };
    }
}