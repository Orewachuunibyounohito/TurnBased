using System;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedPractice.SO
{
    [CreateAssetMenu(menuName = "Turn-based/Audio/SFX Settings", fileName = "NewSFXSettings")]
    public class SfxSettingsSO : ScriptableObject
    {
        public List<SfxData> SfxList;

        [Serializable]
        public class SfxData : IEquatable<SfxData>
        {
            public SfxName   Name;
            public AudioClip Clip;

            public override int  GetHashCode()         => Name.GetHashCode();
            public          bool Equals(SfxData other) => other != null && Equals(other.Name);
        }
    }
}