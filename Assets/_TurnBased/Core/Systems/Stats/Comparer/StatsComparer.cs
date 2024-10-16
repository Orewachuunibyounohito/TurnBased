using System;
using System.Collections.Generic;

namespace TurnBasedPractice.StatsSystem
{   
    // No used
    public class StatsComparer : IComparer<Stats>
    {
        public int Compare(Stats x, Stats y)
        {
            var statsNames = Enum.GetValues(typeof(Stats));
            StatsName statsName;
            foreach(var tempName in statsNames){
                statsName = (StatsName)tempName;
                if(x.ContainsKey(statsName) && y.ContainsKey(statsName)){
                    return x[statsName] - y[statsName];
                }
            }
            return 0;
        }
    }
}