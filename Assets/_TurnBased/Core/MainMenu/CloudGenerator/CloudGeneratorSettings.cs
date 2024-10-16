using System;
using ChuuniExtension.CountdownTool;
using Sirenix.OdinInspector;

namespace TurnBasedPractice.MainMenu
{
    // Generate cloud with "ScaleRandomFactor" each "interval" with factor 
    // or each "distance" with factor until "clouds.Count" equals "Amount"
    [Serializable]
    public class CloudGeneratorSettings
    {
        public CountdownType CountdownType;
        public int Amount = 1;
        public float Speed = 1;
        public float Deviation = 0;
        [ShowIf("IsInterval")]
        public float Interval = 3;
        [ShowIf("IsInterval")]
        public float IntervalRandomFactor = 0;
        [ShowIf("IsDistance")]
        public float Distance = 2;
        [ShowIf("IsDistance")]
        public float DistanceRandomFactor = 0;
        public float ScaleRandomFactor = 0;

        private bool IsInterval => CountdownType == CountdownType.Interval;
        private bool IsDistance => CountdownType == CountdownType.Distance;
    }
}
