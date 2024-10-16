using TurnBasedPractice.SO;
using Sirenix.OdinInspector;
using System;
using TurnBasedPractice.SkillSystem;

namespace TurnBasedPractice.BattleCore
{
    [Serializable]
    public class PlayerConfig
    {
        public string Name;
        public PlayerControllerType ControllerType;
        public SkillName[] Skills;
        [InlineEditor]
        public StatsSO Stats;
        // public ItemName[] Items;
    }
}
