using System.Collections;
using System.Collections.Generic;
using TurnBasedPractice.BattleCore;
using TurnBasedPractice.MainMenu.Views;
using TurnBasedPractice.Resource;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TurnBasedPractice.MainMenu.Presenters
{
    public class PlayerConfigMenuPresenter : MonoBehaviour
    {
        private PlayerConfigMenuView _configView;
        private PlayerConfigMenuView configView{ 
            get{
                if (_configView == null){
                    _configView = SetUpMenu(transform);
                }
                return _configView;
            }
            set => _configView = value;
        }

        private Dictionary<int, InfoSet> infos;
        private InfoSet currentInfo;

        private void OnDestroy(){
            configView.Unbinding();
        }
        
        private PlayerConfigMenuView SetUpMenu(Transform menu){
            configView = new PlayerConfigMenuView(menu);
            infos = new();
            PlayersLoading loading = Resources.Load<PlayersLoading>(SoPath.PLAYERS_CONFIG_PATH);
            for(int index = 0; index < loading.configs.Count; index++){
                Button button = configView.CreateConfigButton();
                button.onClick.AddListener(ConfigSelected(loading, index));

                InfoSet infoSet = configView.CreateInfoContent(loading.configs[index]);
                infos.Add(index, infoSet);
                infoSet.Hide();
            }
            currentInfo = infos[0]?? default;

            configView.onGameStartClick.AddListener(OnGameStartClick);
            configView.onExitClick.AddListener(OnExitClick);
            return configView;
        }

        private UnityAction ConfigSelected(PlayersLoading loading, int index){
            return () => {
                currentInfo?.Hide();
                loading.configIndex = index;
                currentInfo = infos[index];
                currentInfo.Show();
            };
        }

        private void OnGameStartClick(){
            StartCoroutine(EnterBattle());
        }

        private IEnumerator EnterBattle(){
            AsyncOperation async = SceneManager.LoadSceneAsync("TB_Battle");
            async.completed += (async) => OnLoadingCompleted();
            while(!async.isDone){
                yield return null;
            }
        }

        private void OnLoadingCompleted(){
            Debug.Log($"Loading Completed!");
            var initializeAsync = LocalizationSettings.SelectedLocaleAsync;
            if(initializeAsync.IsDone){
                InitializeCompleted(initializeAsync);
            }else{
                initializeAsync.Completed += InitializeCompleted;
            }
        }
        private void InitializeCompleted(AsyncOperationHandle<Locale> async){
            var battleSystem = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
            BattleInitializer.BattleInitialize(battleSystem);
            battleSystem.EnterBattle();
        }

        private void OnExitClick(){
            configView.ClosePlayerConfigMenu();
        }

        public void OpenMenu(){
            configView.OpenPlayerConfigMenu();
        }
    }
}
