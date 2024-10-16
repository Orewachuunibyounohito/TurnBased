using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TurnBasedPractice.Character;
using TurnBasedPractice.SkillSystem;
using UnityEngine;

public class SkillLearnerTest
{
    [Test]
    public void Given_A_Hero_When_LearnSkill_Impact_Then_HeroLearned_ContainsKey_Impact(){
        var hero = new GameObject("Hero").AddComponent<Hero>();

        SkillLearner.HeroLearnSkill(hero, SkillName.Impact);

        Assert.IsTrue(hero.ContainSkill(SkillName.Impact));
    }
}
