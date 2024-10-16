using System.Collections.Generic;
using Sirenix.OdinInspector;
using TurnBasedPractice.StatsSystem;
using UnityEngine;

namespace TurnBasedPractice.SO
{
    [CreateAssetMenu(fileName = "NewStats", menuName = "Turn-based/Data/Stats SO")]
    public class StatsSO : ScriptableObject
    {
        public List<Stats.Data> RequiredStats;
    }
}