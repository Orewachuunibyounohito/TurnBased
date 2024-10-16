using System;
using ChuuniExtension;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace TurnBasedPractice.MainMenu
{
    public class LanguageSelector : MonoBehaviour
    {
        private const string CONFIRM = "Confirm";

        private CustomSwitchPresenter Selector;
        private Button Confirm;
        private Button OutOfPanel;

        private void Awake(){
            Selector = GetComponentInChildren<CustomSwitchPresenter>();
            Confirm = transform.Find(CONFIRM).GetComponent<Button>();
            OutOfPanel = GetComponent<Button>();
        }
        private void Start(){
            Confirm.onClick.AddListener(ChangeLanguage);
            OutOfPanel.onClick.AddListener(OnMenuClosed);
        }


        private void ChangeLanguage(){
            Locale locale = LocalizationSettings.AvailableLocales
                                                .Locales
                                                .Find((item) => item.LocaleName == Selector.CurrentItem);
            if(!locale){ return ; }
            LocalizationSettings.SelectedLocale = locale;
        }

        public void CloseMenu() => gameObject.SetActive(false);
        public void OpenMenu() => gameObject.SetActive(true);
        private void SwitchToCurrentLocale() => Selector.SwitchToCurrentLocale();
        private void OnMenuClosed(){
            CloseMenu();
            SwitchToCurrentLocale();
        }

        #if UNITY_EDITOR
        [Title("---- Debug Mode ----")]
        [Button]
        public void AddItem(string itemName){
            if(string.IsNullOrEmpty(itemName)){ return ; }
            Selector.AddItem(itemName);
        }
        [Button]
        public void DeleteItem(int index){
            if(Selector.IsOutOfRange(index)){ return ; }
            Selector.DeleteItem(index);
        }
        #endif
    }
}