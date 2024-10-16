using System;
using System.Collections.Generic;
using NUnit.Framework;
using TurnBasedPractice.SkillSystem;

public class SkillFactoryTest
{
    [Test]
    public void GivenImpactToSkillFactory(){
        var skill = SkillFactory.GenerateSkill(SkillName.Impact);

        Assert.IsInstanceOf<Impact>(skill);
    }

    [Test]
    public void SkillFactory_GetAllSkills_Then_Count_Is_Two()
    {
        List<Skill> skills = SkillFactory.GetAllSkills();

        Assert.AreEqual(2, skills.Count);
    }

    
}
