using System.Collections.Generic;
using System.Linq;
using TurnBasedPractice.BattleCore;
using TurnBasedPractice.Resource;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedPractice.MainMenu
{
    public class PlayerInfo
    {
        private readonly static GameObject playerInfoTemplate;

        private ScrollRect scrollRect;
        private RectTransform container; // ScrollRect.content
        private List<PlayerInfoItemPresenter> items;
        private bool _visible;

        public bool Visible{
            get => _visible;
            set{
                _visible = value;
                scrollRect.gameObject.SetActive(value);
            }
        }

        static PlayerInfo(){
            playerInfoTemplate = Resources.Load<PrefabsSettings>(SoPath.PREFABS_SETTINGS_PATH)
                                          .PlayerInfoTemplate;
        }

        public PlayerInfo(){
            scrollRect = Object.Instantiate(playerInfoTemplate)
                               .GetComponent<ScrollRect>();
            container = scrollRect.content;
            items = new();
        }

        public PlayerInfo(PlayerConfig playerConfig) : this(){
            PlayerInfoItemPresenter item = PlayerInfoItemPresenter.GenerateNameItem(playerConfig.Name);
            AddItem(item);

            IEnumerable<StatsSystem.Stats.Data> basicStatsDatas = playerConfig.Stats
                                                                              .RequiredStats
                                                                              .Where((statsData) => statsData.Name < StatsSystem.StatsName.Guard);
            foreach (var statsData in basicStatsDatas){
                item = PlayerInfoItemPresenter.GenerateStatsItem(statsData.Name, statsData.Value);
                AddItem(item);
            }
        }

        public void AddItem(PlayerInfoItemPresenter item){
            items.Add(item);
            item.SetContainer(container);
        }
        public void SetParentAndAdjustScale(RectTransform container){
            scrollRect.GetComponent<RectTransform>().SetParent(container);
            scrollRect.GetComponent<RectTransform>().localScale = Vector3.one;
        }

        #if UNITY_EDITOR
        public RectTransform GetContainer() => container;
        public List<PlayerInfoItemPresenter> GetItems() => items;
        #endif
    }
}