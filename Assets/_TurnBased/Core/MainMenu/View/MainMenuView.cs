using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedPractice.MainMenu.Views
{
    public class MainMenuView
    {
        private Transform menuPanel;
        private Button startButton;
        private Button languageButton;
        private Button exitButton;

        public MainMenuView(Transform meunPanel){
            this.menuPanel = meunPanel;
            startButton = meunPanel.Find("StartButton").GetComponent<Button>();
            languageButton = meunPanel.Find("LanguageButton").GetComponent<Button>();
            exitButton = meunPanel.Find("ExitButton").GetComponent<Button>();
        }

        public void PanelActiveSwitch(){
            menuPanel.gameObject.SetActive(!menuPanel.gameObject.activeInHierarchy);
        }
        public Button.ButtonClickedEvent onStartClick => startButton.onClick;
        public Button.ButtonClickedEvent onLanguageClick => languageButton.onClick;
        public Button.ButtonClickedEvent onExitClick => exitButton.onClick;
    }
}
