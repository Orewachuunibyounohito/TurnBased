using TurnBasedPractice.MainMenu;
using UnityEngine;

namespace TurnBasedPractice.BattleCore
{
    public class OptionsUiPresenter : MonoBehaviour
    {
        private const string MENU = "Menu";
        private const string LANGUAGE_MENU = "LanguageMenu";
        private OptionsMenuPanel menuPanel;
        private LanguageSelector languageSelector;

        private void Awake(){
            menuPanel = transform.Find(MENU)
                                 .gameObject
                                 .AddComponent<OptionsMenuPanel>();
            languageSelector = transform.Find(LANGUAGE_MENU)
                                        .gameObject
                                        .AddComponent<LanguageSelector>();
        }

        private void Start(){
            menuPanel.LanguageClicked.AddListener(OnLanguageClicked);
        }

        private void OnLanguageClicked(){
            languageSelector.OpenMenu();
        }
    }
}