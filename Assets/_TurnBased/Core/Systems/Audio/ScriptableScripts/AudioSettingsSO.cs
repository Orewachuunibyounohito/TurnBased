using Sirenix.OdinInspector;
using UnityEngine;

namespace TurnBasedPractice.SO
{
    [CreateAssetMenu(menuName = "Turn-based/Audio/Audio Settings", fileName = "NewAudioSettings")]
    public class AudioSettingsSO : ScriptableObject
    {
        [InlineEditor]
        public BgmSettingsSO bgmSettings;
        [InlineEditor]
        public SfxSettingsSO sfxSettings;
    }
}
