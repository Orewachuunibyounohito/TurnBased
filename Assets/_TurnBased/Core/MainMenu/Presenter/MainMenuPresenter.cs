using TurnBasedPractice.MainMenu.Views;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedPractice.MainMenu.Presenters
{
    public class MainMenuPresenter : MonoBehaviour
    {
        private MainMenuView mainMenuView;

        private void Start(){
            PlayerConfigMenuPresenter playerConfigMenuPresenter = 
                transform.parent
                         .Find("PlayerConfigMenu")
                         .gameObject
                         .AddComponent<PlayerConfigMenuPresenter>();
            LanguageSelector languageSelector = 
                transform.parent
                         .Find("LanguageMenu")
                         .gameObject
                         .AddComponent<LanguageSelector>(); 
            mainMenuView = SetUpMainMenu(transform,
                                         playerConfigMenuPresenter,
                                         languageSelector);
        }

        private MainMenuView SetUpMainMenu(Transform menu,
                                           PlayerConfigMenuPresenter playerConfigMenuPresenter,
                                           LanguageSelector languageSelector)
        {
            MainMenuView result = new MainMenuView(menu);
            result.onStartClick.AddListener(playerConfigMenuPresenter.OpenMenu);
            result.onLanguageClick.AddListener(languageSelector.OpenMenu);
            result.onExitClick.AddListener(OnExitClick);
            return result;
        }

        private void OnExitClick(){
            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }

        public Button.ButtonClickedEvent onStartClick => mainMenuView.onStartClick;
        public Button.ButtonClickedEvent onLanguageClick => mainMenuView.onLanguageClick;
    }
}
