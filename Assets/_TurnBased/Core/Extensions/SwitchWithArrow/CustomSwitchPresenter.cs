using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;

namespace ChuuniExtension
{
    public class CustomSwitchPresenter : MonoBehaviour
    {
        private CustomSwitchModel model;
        private CustomSwitchView view;

        public string CurrentItem => model.CurrentItem;

        private void Awake(){
            model = new CustomSwitchModel();
            view = new CustomSwitchView(gameObject.transform);
            gameObject.AddComponent<EventTrigger>();
        }


        private void Start(){
            foreach(var locale in LocalizationSettings.AvailableLocales.Locales){
                AddItem(locale.LocaleName);
            }
            model.ItemChanged.AddListener(UpdateView);
            view.PreviousClicked.AddListener(OnPreviousClick);
            view.NextClicked.AddListener(OnNextClick);

            InitSwitch();
        }


        public void AddItem(string value){
            model.Add(value);
            view.Add(value);
        }

        private void InitSwitch(){
            model.InitByCurrentLocale();
            view.InitByCurrentLocale();
        }
        public void UpdateView(CustomSwitchCallback callback) => view.UpdateView(callback);
        private void OnPreviousClick() => model.PreviousItem();
        private void OnNextClick() => model.NextItem();

        public bool IsOutOfRange(int index) => model.IsOutOfRange(index);
        public void SwitchToCurrentLocale() => model.SwitchToCurrentLocale();

        public void DeleteItem(int index){
            model.DeleteItem(index);
            view.DeleteItem(index);
        }

    }
}