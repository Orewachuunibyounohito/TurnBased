using System;
using TurnBasedPractice.Character;

namespace TurnBasedPractice.SkillSystem
{
    public class SkillLearner
    {
        public static bool HeroLearnSkill(Hero hero, SkillName skillName)
        {
            if (hero == null){ return false; }
            if (skillName == SkillName.None){ return false; }

            return hero.LearnSkill(SkillFactory.GenerateSkill(skillName));
        }
    }
}