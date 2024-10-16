using UnityEngine;
using TurnBasedPractice.SO;
using Sirenix.OdinInspector;
using TurnBasedPractice.SkillSystem;
using TurnBasedPractice.Resource;
using TurnBasedPractice.Items;

namespace TurnBasedPractice.BattleCore
{
    [CreateAssetMenu(menuName = "Turn-based/Player/Settings", fileName = "New Settings")]
    public class PlayersSettings : ScriptableObject
    {
        public static PlayerConfig[] DefaultConfigs;

        [ShowInInspector]
        public PlayerConfig player1, player2;

        private void OnEnable(){
            if (DefaultConfigs != null) { return; }
            GenerateDefaultConfigs();
        }

        private static void GenerateDefaultConfigs(){
            StatsSO defaultStats = Resources.Load<StatsSO>(SoPath.DEFAULT_HERO_STATS_PATH);
            DefaultConfigs = new PlayerConfig[]{
                new PlayerConfig{
                    Name = "Hero1",
                    ControllerType = PlayerControllerType.Human,
                    Skills = new SkillName[]{ SkillName.Impact, SkillName.StaticElectricity },
                    Stats = defaultStats
                },
                new PlayerConfig{
                    Name = "Hero2",
                    ControllerType = PlayerControllerType.NonHuman,
                    Skills = new SkillName[]{ SkillName.WaterBall },
                    Stats = defaultStats
                }
            };
        }

    }
}
