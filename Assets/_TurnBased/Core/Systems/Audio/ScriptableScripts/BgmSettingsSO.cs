using System;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedPractice.SO
{
    [CreateAssetMenu(menuName = "Turn-based/Audio/BGM Settings", fileName = "NewBGMSettings")]
    public class BgmSettingsSO : ScriptableObject
    {
        public List<BgmData> BgmList;

        [Serializable]
        public class BgmData : IEquatable<BgmData>
        {
            public BgmName   Name;
            public AudioClip Clip;

            public override int  GetHashCode()         => Name.GetHashCode();
            public          bool Equals(BgmData other) => other != null && Equals(other.Name);
        }
    }
}
