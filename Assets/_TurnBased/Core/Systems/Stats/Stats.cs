using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TurnBasedPractice.StatsSystem
{
    [Serializable]
    public class Stats : IComparable<Stats>
    {
        [ShowInInspector]
        private Dictionary<StatsName, Data> Datas;

        public int this[StatsName statsName]{
            get{
                if(Datas.ContainsKey(statsName)){ return Datas[statsName].Value; }
                return 0;
            }
            set{
                if(Datas.ContainsKey(statsName)){ Datas[statsName].Value = value; }
            }
        }
        public ICollection<StatsName> Keys                      => Datas.Keys;
        public ICollection<Data>      Values                    => Datas.Values;
        public int                    Count                     => Datas.Count;

        public void Add(Data data)                                        => Datas.Add(data.Name, new Data(data.Name, data.Value));
        public void Add(StatsName key, int  value)                        => Datas.Add(key, new Data(key, value));
        public void Clear()                                               => Datas.Clear();
        public bool ContainsKey(StatsName key)                            => Datas.ContainsKey(key);
        public bool Remove(StatsName key)                                 => Datas.Remove(key);
        public bool TryGetValue(StatsName key, out Data value)            => Datas.TryGetValue(key, out value);
        public IEnumerator<KeyValuePair<StatsName, Data>> GetEnumerator() => Datas.GetEnumerator();

        public int CompareTo(Stats other)
        {
            StatsName statsName;
            foreach(var stat in Enum.GetValues(typeof(StatsName))){
                statsName = (StatsName)stat;
                if(ContainsKey(statsName) && other.ContainsKey(statsName)){
                    return other[statsName] - this[statsName];
                }
            }
            Debug.LogWarning($"No matched statsData, cannot compared.");
            return other.Count - Count;
        }

        public Stats(){
            Datas = new Dictionary<StatsName, Data>();
        }
        public Stats(params Data[] datas){
            Datas = new Dictionary<StatsName, Data>();
            foreach(var data in datas){
                Add( data );
            }
        }
        public override string ToString(){
            var result = "[";
            foreach(var data in Datas.Values){
                result += $"{Enum.GetName(typeof(StatsName), data.Name)}, ";
            }
            result = result.Length > 1? result.Remove(result.Length - 2) + "]" : "[]";
            return result;
        }

        [Serializable]
        public class Data{
            public StatsName Name;
            public int       Value;

            public Data(StatsName statsName, int value){
                Name  = statsName;
                Value = value;
            }
        }
    }
}