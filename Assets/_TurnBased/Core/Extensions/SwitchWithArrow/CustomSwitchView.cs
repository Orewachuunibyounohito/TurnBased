using System;
using System.Collections.Generic;
using TMPro;
using TurnBasedPractice;
using TurnBasedPractice.Resource;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace ChuuniExtension
{
    [Serializable]
    public class CustomSwitchView
    {
        private const string CONTENT = "Viewport/Content";
        private const string PREVIOUS_ARROW = "PreviousArrow";
        private const string NEXT_ARROW = "NextArrow";

        private static GameObject itemPrefab;

        private List<TextMeshProUGUI> itemTexts;
        private RectTransform content;
        private Button previous;
        private Button next;

        public Button.ButtonClickedEvent PreviousClicked => previous.onClick;
        public Button.ButtonClickedEvent NextClicked => next.onClick;

        static CustomSwitchView(){
            itemPrefab = Resources.Load<PrefabsSettings>(SoPath.PREFABS_SETTINGS_PATH)
                                  .SwitchItemTemplate;
        }

        public CustomSwitchView(Transform switchPanel){
            itemTexts = new List<TextMeshProUGUI>();
            content = switchPanel.Find(CONTENT).GetComponent<RectTransform>();
            previous = switchPanel.Find(PREVIOUS_ARROW).GetComponent<Button>();
            next = switchPanel.Find(NEXT_ARROW).GetComponent<Button>();
        }

        public void UpdateView(CustomSwitchCallback callback){
            if(callback.PreviousIndex != -1){
                itemTexts[callback.PreviousIndex].gameObject.SetActive(false);
            }
            itemTexts[callback.Index].gameObject.SetActive(true);
            ActivateNextButton();
            ActivatePreviousButton();
            if(callback.IsFirst){ DeactivatePreviousButton(); }
            if(callback.IsLast){ DeactivateNextButton(); }
        }

        public void Add(string value){
            var itemObject = Object.Instantiate(itemPrefab, content);
            var itemText = itemObject.GetComponent<TextMeshProUGUI>();
            itemText.SetText(value);
            itemText.gameObject.SetActive(false);
            itemTexts.Add(itemText);
            ActivateNextButton();
        }
        public void SwitchByIndex(int index){
            itemTexts[index]?.gameObject.SetActive(true);
        }
        public void InitByCurrentLocale(){
            int currentIndex = LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
            itemTexts[currentIndex]?.gameObject.SetActive(true);
        }

        public void ActivateNextButton() => next.interactable = true;
        public void DeactivateNextButton() => next.interactable = false;
        public void ActivatePreviousButton() => previous.interactable = true;
        public void DeactivatePreviousButton() => previous.interactable = false;

        public void DeleteItem(int index){
            Object.Destroy(itemTexts[index].gameObject);
            itemTexts.RemoveAt(index);
        }

    }
}