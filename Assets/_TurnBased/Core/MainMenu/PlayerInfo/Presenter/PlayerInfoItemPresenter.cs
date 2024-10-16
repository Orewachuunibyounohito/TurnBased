using TurnBasedPractice.StatsSystem;
using UnityEngine;

namespace TurnBasedPractice.MainMenu
{
    public class PlayerInfoItemPresenter
    {
        private PlayerInfoItem item;
        private PlayerInfoItemView view;
        #if UNITY_EDITOR
        public GameObject gameObject => view.gameObject;
        public (string label, string value) ItemContent => item.GetContent();
        #endif

        public void SetContainer(Transform container)=> view.SetParant(container);
        public override string ToString() => $"{item}\n{view}";

        public static PlayerInfoItemPresenter GenerateNameItem(string name){
            return new PlayerInfoItemPresenter{
                item = PlayerInfoItem.GenerateNameItem(name),
                view = PlayerInfoItemView.Generate("Name", name)
            };
        }

        public static PlayerInfoItemPresenter GenerateStatsItem(StatsName name, int value){
            return new PlayerInfoItemPresenter{
                item = PlayerInfoItem.GenerateStatsItem(name, value),
                view = PlayerInfoItemView.Generate($"{name}", $"{value}")
            };
        }
    }
}