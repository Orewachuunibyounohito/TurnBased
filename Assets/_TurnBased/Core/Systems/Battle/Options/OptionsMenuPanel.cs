using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedPractice.BattleCore
{  
    public class OptionsMenuPanel : MonoBehaviour
    {
        private OptionsMenuPanelView view;

        public Button.ButtonClickedEvent LanguageClicked => view.LanguageClicked;

        private void Awake(){
            view = new OptionsMenuPanelView(transform);
        }
    }

    public class OptionsMenuPanelView
    {
        private const string LANGUAGE_BUTTON = "LanguageButton";
        private Button languageButton;

        public Button.ButtonClickedEvent LanguageClicked => languageButton.onClick;

        public OptionsMenuPanelView(Transform panel){
            languageButton = panel.Find(LANGUAGE_BUTTON).GetComponent<Button>();
        }
    }
}