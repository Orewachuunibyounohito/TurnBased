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
    public class AbstractEffect : IEffect
    {
        protected int baseDuration = 5;
        protected int baseSuccessRate = 20;

        protected string bestowedTemplate{ get => LocalizationSettings.GetBuffString(Name, BuffPhase.Bestowed).GetLocalizedString(); }
        protected string effectTemplate{ get => LocalizationSettings.GetBuffString(Name, BuffPhase.Effect).GetLocalizedString(); }
        protected string liftTemplate{ get => LocalizationSettings.GetBuffString(Name, BuffPhase.Lift).GetLocalizedString(); }

        [ShowInInspector, ReadOnly]
        public virtual EffectName  Name  { get; }
        public virtual EffectType  Type  { get; }
        public virtual EffectOrder Order { get; }

        public virtual bool IsOvertime { get; }
        [ShowInInspector, ReadOnly]
        public virtual int Duration { get; set; }

        public AbstractEffect(){
            Duration = baseDuration;
        }

        public virtual bool Bestow(Hero user, Hero target/* , Func<float> probability */){
            var value            = Random.Range(0f, 100f);
            var successThreshold = Mathf.Max(baseSuccessRate, 0);
            return value <= successThreshold;
        }

        public virtual bool Bestow(Hero user, Hero target, Factor thresholdFactor){
            var value            = Random.Range(0f, 100f);
            var baseSuccess      = Mathf.Max(baseSuccessRate, 0);
            var successThreshold = Mathf.FloorToInt(thresholdFactor.Calculate(baseSuccess));
            return value <= successThreshold;
        }

        public virtual void Effect(Hero owner){
            Duration--;
            BattleInfoSystem.Add(string.Format(effectTemplate, owner.Name));
        }

        public virtual void Lift(Hero owner){
            BattleInfoSystem.Add(string.Format(liftTemplate, owner.Name));
        }

        public virtual string BestowedText(Hero target) =>
            string.Format(bestowedTemplate, target.Name);
        public virtual string LiftText(Hero owner) =>
            string.Format(liftTemplate, owner.Name);
    }
}