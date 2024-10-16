using System;
using TurnBasedPractice.Character;

namespace TurnBasedPractice.Effects
{
    public static class EffectSystem
    {
        public static string Bestow(IEffect effect, Hero user, Hero target){
            string extraInfo = default;
            if(effect.Bestow(user, target)){
                target.Buff.AddBuff(effect);
                extraInfo = $"\n{effect.BestowedText(target)}";
            }
            return extraInfo;
        }

        public static string Bestow(IEffect effect, Hero user, Hero target, Factor thresholdFactor){
            string extraInfo = default;
            if(effect.Bestow(user, target, thresholdFactor)){
                target.Buff.AddBuff(effect);
                extraInfo = $"\n{effect.BestowedText(target)}";
            }
            return extraInfo;
        }
    }
}