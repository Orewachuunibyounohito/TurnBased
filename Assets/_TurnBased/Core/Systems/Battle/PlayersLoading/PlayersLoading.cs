using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TurnBasedPractice.SO;
using System.Linq;

namespace TurnBasedPractice.BattleCore
{
    [CreateAssetMenu(menuName = "Turn-based/Player/PlayersLoading", fileName = "New Loading")]
    public class PlayersLoading : ScriptableObject
    {
        private static int[] indexes;
        
        [LabelText("載入角色配置")]
        public bool LoadCustomConfig;

        [ValueDropdown("indexes")]
        public int configIndex = -1;

        [InlineEditor]
        public List<PlayersSettings> configs = new List<PlayersSettings>();
        
        public PlayerConfig P1Config() => configs[configIndex].player1;
        public PlayerConfig P2Config() => configs[configIndex].player2;
        
        private void OnValidate(){
            var indexList = new List<int>();
            for(int count = 0; count < configs.Count; count++){ indexList.Add(count); }
            indexes = indexList.ToArray();
        }
    }
}
