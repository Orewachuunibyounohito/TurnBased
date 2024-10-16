using Sirenix.OdinInspector;
using TurnBasedPractice.BattleCore.Loggers;
using UnityEngine;

namespace TurnBasedPractice.DebugTools
{
    public class DebugTool : MonoBehaviour
    {
        [Button("Show Log")]
        public void ShowLog(){
            LoggerSystem.ShowLogForUnity();
        }
    }
}