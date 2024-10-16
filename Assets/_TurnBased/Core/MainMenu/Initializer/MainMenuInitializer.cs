using System;
using TMPro;
using TurnBasedPractice.AudioSystem;
using TurnBasedPractice.MainMenu.Presenters;
using TurnBasedPractice.SO;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TurnBasedPractice.MainMenu
{
    public class MainMenuInitializer : MonoBehaviour
    {
        [SerializeField]
        private GameObject mainMenu;

        private void Start(){
            EnsureSelectedLocaleIsDone();
        }

        private void EnsureSelectedLocaleIsDone(){
            var async = LocalizationSettings.SelectedLocaleAsync;
            if(async.IsDone){
                CompletedTask(async);
            }else{
                async.Completed += CompletedTask;
            }
        }
        private void CompletedTask(AsyncOperationHandle<Locale> async){
            AudioPlayer.PlayBgm(BgmName.MainMenu);
            mainMenu.AddComponent<MainMenuPresenter>();
        }
    }
}