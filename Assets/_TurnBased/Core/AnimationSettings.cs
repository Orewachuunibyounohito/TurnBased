using Sirenix.OdinInspector;
using UnityEngine;


namespace TurnBasedPractice
{
    [CreateAssetMenu(menuName = "Turn-based/AnimationSettings/AnimationClips", fileName = "New Clips")]
    public class AnimationSettings : ScriptableObject
    {
        private const string HINT_GROUP = "Hint";

        [FoldoutGroup(HINT_GROUP)]
        public AnimationClip HintFadeInClip;
        [FoldoutGroup(HINT_GROUP)]
        public AnimationClip HintFadeOutClip;
    }
}
