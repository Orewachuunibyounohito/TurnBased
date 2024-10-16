using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace ChuuniExtension
{
    [Serializable]
    public class CustomSwitchModel
    {   
        private List<string> items;
        private int _currentIndex;
        private int previousIndex;
        private int currentIndex{
            get => _currentIndex;
            set{
                previousIndex = _currentIndex;
                _currentIndex = value;
                if(_currentIndex != -1){
                    ItemChanged?.Invoke(Callback());
                }
            }
        }

        public bool IsEmpty => items.Count == 0;
        public bool IsFirst => currentIndex == 0;
        public bool IsLast => currentIndex == items.Count-1;

        public string CurrentItem => items[currentIndex];

        public UnityEvent<CustomSwitchCallback> ItemChanged;

        public CustomSwitchModel(){
            items = new List<string>();
            currentIndex = -1;
            previousIndex = -1;
            ItemChanged = new UnityEvent<CustomSwitchCallback>();
        }

        public void PreviousItem(){
            if(IsEmpty){ return; }
            if(IsFirst){ return ; }
            previousIndex = currentIndex;
            currentIndex--;
        }

        public void NextItem(){
            if(IsEmpty){ return; }
            if(IsLast){ return ; }
            previousIndex = currentIndex;
            currentIndex++;
        }

        private CustomSwitchCallback Callback() => new CustomSwitchCallback(
            previousIndex,
            currentIndex,
            CurrentItem,
            IsFirst,
            IsLast
        );

        public void Add(string value){
            items.Add(value);
        }

        public void SwitchByIndex(int index) => currentIndex = index;
        public void SwitchToCurrentLocale() => currentIndex = items.IndexOf(LocalizationSettings.SelectedLocale.LocaleName);
        public void InitByCurrentLocale() => currentIndex = items.IndexOf(LocalizationSettings.SelectedLocale.LocaleName);

        public bool IsOutOfRange(int index) => index >= items.Count || index < 0;
        public void DeleteItem(int index){
            items.RemoveAt(index);
            if(index <= currentIndex){
                currentIndex--;
            }
        }
    }
}