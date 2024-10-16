using System.Collections.Generic;
using TMPro;
using TurnBasedPractice.BattleCore;
using TurnBasedPractice.Localization;
using TurnBasedPractice.MainMenu.Presenters;
using TurnBasedPractice.Resource;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;
using UnityLocalizationSettings = UnityEngine.Localization.Settings.LocalizationSettings;

namespace TurnBasedPractice.MainMenu.Views
{
    public class PlayerConfigMenuView
    {
        private GameObject _configButtonPrefab;
        private GameObject configButtonPrefab{
            get{
                if(_configButtonPrefab == null){
                    _configButtonPrefab = Resources.Load<PrefabsSettings>(SoPath.PREFABS_SETTINGS_PATH).ConfigButton;
                }
                return _configButtonPrefab;
            }
        }

        private Transform menu;
        private RectTransform selectorContainer;
        private List<Button> configButtons;
        private RectTransform p1InfoContainer;
        private RectTransform p2InfoContainer;
        private Button gameStartButton;
        private Button exitButton;

        public Button.ButtonClickedEvent onGameStartClick => gameStartButton.onClick;
        public Button.ButtonClickedEvent onExitClick => exitButton.onClick;

        public PlayerConfigMenuView(Transform menu)
        {
            this.menu = menu;
            selectorContainer = menu.Find("ConfigPanel/Selector").GetComponentInChildren<ScrollRect>().content;
            p1InfoContainer = menu.Find("ConfigPanel/PlayerInfo/P1").GetComponent<RectTransform>();
            p2InfoContainer = menu.Find("ConfigPanel/PlayerInfo/P2").GetComponent<RectTransform>();
            gameStartButton = menu.Find("ConfigPanel/GameStartButton").GetComponent<Button>();
            exitButton = menu.Find("ConfigPanel/ExitButton").GetComponent<Button>();
            configButtons = new List<Button>();
            LocalizedString localizedString = LocalizationSettings.GetCommonString(CommonStringTemplate.PlayerConfigLabel);
            localizedString.StringChanged += UpdateButtonsString;
        }

        public void Unbinding(){
            var localizedString = LocalizationSettings.GetCommonString(CommonStringTemplate.PlayerConfigLabel);
            localizedString.StringChanged -= UpdateButtonsString;
        }

        public void OpenPlayerConfigMenu() => menu.gameObject.SetActive(true);
        public void ClosePlayerConfigMenu() => menu.gameObject.SetActive(false);
        public Button CreateConfigButton(){
            Button configButton = Object.Instantiate(configButtonPrefab, selectorContainer)
                                        .GetComponent<Button>();
            configButtons.Add(configButton);
            SetButtonText(configButton);
            return configButton;
        }

        private void SetButtonText(Button configButton){
            var localizedString = LocalizationSettings.GetCommonString(CommonStringTemplate.PlayerConfigLabel);
            var index = configButtons.IndexOf(configButton);
            var localizedNumber = Helper.NumberToLocale((ulong)index+1);
            string label = string.Format(localizedString.GetLocalizedString(), localizedNumber);
            configButton.GetComponentInChildren<TextMeshProUGUI>().SetText(label);
        }

        private void UpdateButtonsString(string translatedString){
            for(int index = 0; index < configButtons.Count; index++){
                string fullString = string.Format(translatedString, Helper.NumberToLocale((ulong)index+1));
                configButtons[index].GetComponentInChildren<TextMeshProUGUI>()
                                    .SetText(fullString);
            }
        }
        
        public InfoSet CreateInfoContent(PlayersSettings settings){
            var p1Info = new PlayerInfo(settings.player1);
            var p2Info = new PlayerInfo(settings.player2);
            p1Info.SetParentAndAdjustScale(p1InfoContainer);
            p2Info.SetParentAndAdjustScale(p2InfoContainer);
            return new InfoSet(p1Info, p2Info);
        }
    }
}
