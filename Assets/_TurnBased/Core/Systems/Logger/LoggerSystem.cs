using System;
using System.Collections.Generic;
using TurnBasedPractice.Character.Controller;
using UnityEngine;

namespace TurnBasedPractice.BattleCore.Loggers
{
    public class LoggerSystem
    {
        private static List<string> logs = new List<string>();

        public static void Add(string log){
            logs.Add(log);
        }
        
        public static void ShowLogForUnity(){
            string info = "";
            foreach(var log in logs){
                info += $"{log}\n";
            }

            if(string.IsNullOrEmpty(info)){
                Debug.Log("No log.");
            }else{
                info += "----";
                Debug.Log(info);
            }
        }
    
        public static void EnterForBattleState(BattleStateType type){
            Add($"-- Battle State -- [{DateTime.Now.ToLocalTime()}]");
            Add($"Enter {type}");
        }
        public static void ExitForBattleState(BattleStateType type){
            Add($"-- Battle State -- [{DateTime.Now.ToLocalTime()}]");
            Add($"Exit {type}");
        }

        public static void EnterForControlState(ControlStateType type){
            Add($"-- Control State -- [{DateTime.Now.ToLocalTime()}]");
            Add($"Enter {type}");
        }
        public static void ExitForControlState(ControlStateType type){
            Add($"-- Control State -- [{DateTime.Now.ToLocalTime()}]");
            Add($"Exit {type}");
        }
    }
}