using UnityEngine;
using TurnBasedPractice.StatsSystem;
using TurnBasedPractice.BattleCore.Selection;

using NUnit.Framework;
using System.Collections.Generic;

namespace TurnBasedPractice.Test
{
    public class TurnBasedTest
    {
        private Stats.Data hpData, mpData, strengthData, wisdomData, defenseData;

        [SetUp]
        public void SetUpCase(){
            hpData       = new Stats.Data(StatsName.MaxHp, 10);
            mpData       = new Stats.Data(StatsName.MaxMp, 10);
            strengthData = new Stats.Data(StatsName.Strength, 10);
            wisdomData   = new Stats.Data(StatsName.Wisdom, 10);
            defenseData  = new Stats.Data(StatsName.Defense, 10);
        }

        [Test]
        public void StatsComparerForSortIsCorrect(){
            var stats1 = new Stats();
            var stats2 = new Stats(strengthData, wisdomData);

            List<Stats> statsList = new List<Stats>{
                stats1,
                stats2
            };
            statsList.Sort();
            statsList.ForEach((stats) => Debug.Log(stats));

            Assert.AreEqual(new List<Stats>{ stats2, stats1 }, statsList);
        }

        [Test]
        // BDD named
        public void Given_AnInterface_When_IsClass_Then_AsClass(){
            // Arrange
            // IFocusable    focus  = new NonMonoDefenseAction();
            IFocusable focus = SelectionFactory.GenerateDefenseSelection(SelectionData.DefenseSelectionData(null, null)) as IFocusable;
            ICustomAction action = default;

            // Act
            if(focus is ICustomAction){ action = (ICustomAction)focus; }
            Debug.Log($"{action}");

            // Assert
            Assert.IsNotNull(action);
        }
    }
}
