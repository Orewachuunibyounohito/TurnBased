using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TurnBasedPractice.Character;
using TurnBasedPractice.InfoSystem;
using UnityEngine;

namespace TurnBasedPractice.Effects
{
    [Serializable]
    public class Buff
    {
        [ShowInInspector]
        private Dictionary<EffectType, List<IEffect>> _preProcessBuff  = new();
        [ShowInInspector]
        private Dictionary<EffectType, List<IEffect>> _postProcessBuff = new();
        private Hero _owner;

        public Buff(Hero owner){
            _owner = owner;
            _preProcessBuff.Add(EffectType.Buff,   new List<IEffect>());
            _preProcessBuff.Add(EffectType.Debuff, new List<IEffect>());
            _postProcessBuff.Add(EffectType.Buff,   new List<IEffect>());
            _postProcessBuff.Add(EffectType.Debuff, new List<IEffect>());
        }

        public void AddBuff(IEffect buff){
            List<IEffect> buffs = buff.Order switch{
                EffectOrder.PreProcess => _preProcessBuff[buff.Type],
                EffectOrder.PostProcess => _postProcessBuff[buff.Type],
                _ => throw new Exception("EffectOrder Error!")
            };

            var  existedBuff = buffs.Find((effect) => effect.Name == buff.Name );
            bool hadBuff     = existedBuff != null;
            if(hadBuff && buff.IsOvertime){
                existedBuff.Duration += buff.Duration;
                return ;
            }
            buffs.Add(buff);
        }

        public void PreProcessEffect(){
            BattleInfoSystem.Clear();
            DoEffect(_preProcessBuff[EffectType.Buff]);
            DoEffect(_preProcessBuff[EffectType.Debuff]);
            if(BattleInfoSystem.HasInfo()){
                BattleInfoSystem.Play();
            }
        }
        public void PostProcessEffect(){
            BattleInfoSystem.Clear();
            DoEffect(_postProcessBuff[EffectType.Buff]);
            DoEffect(_postProcessBuff[EffectType.Debuff]);
            if(BattleInfoSystem.HasInfo()){
                BattleInfoSystem.Play();
            }
        }

        private void DoEffect(List<IEffect> effects){
            var liftAfterDoneList = new List<IEffect>();
            foreach (var effect in effects){
                effect.Effect(_owner);
                if (effect.Duration == 0) { liftAfterDoneList.Add(effect); }
            }
            LiftBuffs(effects, liftAfterDoneList.ToArray());
        }

        private void LiftBuffs(List<IEffect> owned, params IEffect[] lifts){
            foreach(var liftedBuff in lifts){
                liftedBuff.Lift(_owner);
                owned.Remove(liftedBuff);
            }
        }

        public List<IEffect> GetPositiveBuffs(){
            List<IEffect> positiveBuffs = new List<IEffect>();
            positiveBuffs.AddRange(_preProcessBuff[EffectType.Buff]);
            positiveBuffs.AddRange(_postProcessBuff[EffectType.Buff]);
            return positiveBuffs;
        }
        public List<IEffect> GetNegativeBuffs(){
            List<IEffect> negativeBuffs = new List<IEffect>();
            negativeBuffs.AddRange(_preProcessBuff[EffectType.Debuff]);
            negativeBuffs.AddRange(_postProcessBuff[EffectType.Debuff]);
            return negativeBuffs;
        }
        public void ShortenBuff(EffectName effectName, int amount){
            IEffect effect = EffectFactory.Generate(effectName);
            
            List<IEffect> buffs = effect.Order switch{
                EffectOrder.PreProcess => _preProcessBuff[effect.Type],
                EffectOrder.PostProcess => _postProcessBuff[effect.Type],
                _ => new List<IEffect>()
            };
            if(buffs.Count == 0){ return ; }

            effect = buffs.Find((effect) => effectName == effect.Name);
            effect.Duration = Math.Max(effect.Duration-amount, 0);
            if(effect.Duration == 0){
                LiftBuffs(buffs, effect);
            }
        }
    }
}