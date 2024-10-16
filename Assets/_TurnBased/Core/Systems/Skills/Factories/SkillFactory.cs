using System;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedPractice.SkillSystem
{
    public static class SkillFactory
    {
        private static readonly Dictionary<SkillName, ISkillFactory> skillFactories;

        static SkillFactory(){
            skillFactories = new Dictionary<SkillName, ISkillFactory>(){
                { SkillName.Impact, new ImpactFactory() },
                { SkillName.WaterBall, new WaterBallFactory() },
                { SkillName.StaticElectricity, new StaticElectricityFactory() },
                { SkillName.Escape, new EscapeFactory() }
            };
        }

        public static Skill GenerateSkill(SkillName skillName){
            return skillFactories[skillName].GenerateSkill();
        }

        public static List<Skill> GetAllSkills(){
            var skills         = new List<Skill>();
            var skillNameArray = Enum.GetValues(typeof(SkillName));

            Skill skill;
            for (int i = 1; i < skillNameArray.Length; i++){
                skill = GenerateSkill((SkillName)skillNameArray.GetValue(i));
                skills.Add(skill);
            }

            return skills;
        }
    }
}