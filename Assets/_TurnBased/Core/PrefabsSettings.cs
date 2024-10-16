using Sirenix.OdinInspector;
using UnityEngine;

namespace TurnBasedPractice
{
    [CreateAssetMenu(menuName = "Turn-based/PrefabsSettings", fileName = "New Settings")]
    public class PrefabsSettings : ScriptableObject
    {
        private const string MAIN_MENU_GROUP = "MainMenu";
        private const string PLAYER_CONFIG_GROUP = "MainMenu/PlayerConfig";
        private const string LANGUAGE_PANEL_GROUP = "MainMenu/Language";
        private const string BATTLE_GROUP = "Battle";
        private const string BATTLE_SKILL_GROUP = "Battle/Skill";

        private const string MAIN_MENU_TEST_GROUP = "MainMenuTest";
        private const string PLAYER_CONFIG_TEST_GROUP = "MainMenuTest/PlayerConfig";

        [FoldoutGroup(MAIN_MENU_GROUP)]
        public GameObject Cloud;
        [FoldoutGroup(MAIN_MENU_GROUP)]
        [FoldoutGroup(PLAYER_CONFIG_GROUP)]
        public GameObject ConfigButton;
        [FoldoutGroup(PLAYER_CONFIG_GROUP)]
        public GameObject PlayerInfoTemplate;
        [FoldoutGroup(PLAYER_CONFIG_GROUP)]
        public GameObject PlayerInfoItemTemplate;
        // [FoldoutGroup(LANGUAGE_PANEL_GROUP)]
        // public GameObject SwitchTemplate;
        [FoldoutGroup(LANGUAGE_PANEL_GROUP)]
        public GameObject SwitchItemTemplate;

        [FoldoutGroup(BATTLE_GROUP)]
        [FoldoutGroup(BATTLE_SKILL_GROUP)]
        public GameObject NotEnoughHint;


        [FoldoutGroup(MAIN_MENU_TEST_GROUP)]
        public GameObject Canvas;
        [FoldoutGroup(PLAYER_CONFIG_TEST_GROUP)]
        public GameObject PlayerInfoPanel;
    }
}