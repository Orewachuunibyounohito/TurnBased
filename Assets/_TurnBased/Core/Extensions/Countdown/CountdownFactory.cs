using System;

namespace ChuuniExtension.CountdownTool
{
    public class CountdownFactory
    {
        public static ICountdown Generate(CountdownType type, CountdownConfig config){
            return type switch{
                CountdownType.Interval => new TimeCountdown(config.Interval),
                CountdownType.Distance => new DistanceCountdown(config.Speed, config.Distance),
                _ => throw new Exception("Unrecognized CountdownType")
            };
        }
    }
}
