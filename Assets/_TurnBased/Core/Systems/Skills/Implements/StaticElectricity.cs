using System;
using System.Collections.Generic;
using TurnBasedPractice.Character;
using TurnBasedPractice.Effects;
using UnityEngine;

namespace TurnBasedPractice.SkillSystem
{
    [Serializable]
    public class StaticElectricity : Skill
    {
        public override SkillName skillName => SkillName.StaticElectricity;

        [SerializeField]
        private List<EffectName> extraEffects;

        public StaticElectricity() : base(){
            Init();
        }

        private void Init(){
            // Name = "StaticElectricity";
            Description = "Basic electric skill, target probably suffer paralysis.";
            ExecuteTime = 2;
            extraEffects = new (){ EffectName.Paralysis };
        }

        public override void Do(Hero user, params Hero[] targets){
            user.Controller.GetSelected().ExecuteInfo = UseText(user);

            int damage;
            foreach(Hero target in targets){
                damage = CalculateDamage();
                target.TakeDamage(damage);
                target.OnHurt(user);

                user.Controller.GetSelected().ExecuteInfo += "\n"+AttackText(target, damage);

                HandleEffectBestow(user, target);
            }

            base.Do(user, targets);
        }

        public override int HandleGuard(int damage, Hero sufferer) => damage;

        private int CalculateDamage() => 5;

        private void HandleEffectBestow(Hero user, Hero target){
            foreach(var effectName in extraEffects){
                IEffect effect = EffectFactory.Generate(effectName);
                string extraInfo = EffectSystem.Bestow(effect, user, target);
                if(!string.IsNullOrEmpty(extraInfo)){
                    user.Controller.GetSelected().ExecuteInfo += extraInfo;
                }
            }
        }
    }
}