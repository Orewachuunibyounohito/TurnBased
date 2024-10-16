using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Systems.Persistance
{
    public class SaveLoadSystem : MonoBehaviour
    {
        [SerializeField]
        public GameData gameData;

        private IDataService dataService;

        private void Awake(){
            dataService = new FileDataService(new JsonSerializer());
        }

        public void NewGame(){
            gameData = new GameData(){
                Name             = "New Game",
                CurrentLevelName = "Demo"
            };
            SceneManager.LoadScene(gameData.CurrentLevelName);
        }

        [Button(Name = "Save Game")]
        public void SaveGame(){
            dataService.Save(gameData);
        }

        [Button(Name = "Load Game")]
        public void LoadGame(string gameName){
            gameData = dataService.Load(gameName);

            if(string.IsNullOrWhiteSpace(gameData.CurrentLevelName)){
                gameData.CurrentLevelName = "Demo";
            }

            SceneManager.LoadScene(gameData.CurrentLevelName);
        }

        public void ReloadGame()                => LoadGame(gameData.Name);

        [Button(Name = "Delete Game")]
        public void DeleteGame(string gameName) => dataService.Delete(gameName);
    }

    [Serializable]
    public class GameData
    {
        public string Name;
        public string CurrentLevelName;
    }
}
