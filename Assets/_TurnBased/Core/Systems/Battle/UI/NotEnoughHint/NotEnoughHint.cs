using System;
using System.Linq;
using TMPro;
using TurnBasedPractice.Resource;
using UnityEngine;


namespace TurnBasedPractice.Character
{
    public class NotEnoughHint
    {
        private const string BATTLE_UI = "BattleUI";
        private const string TEXT_TEMPLATE = "\"{0}\"不足";
        private static Transform parent;
        private static GameObject prefab;

        private View view;

        static NotEnoughHint(){
            parent = GameObject.Find(BATTLE_UI).transform;

            var prefabSettings = Resources.Load<PrefabsSettings>(SoPath.PREFABS_SETTINGS_PATH);
            prefab = prefabSettings.NotEnoughHint;
        }

        public NotEnoughHint(Skill skill){
            var viewObject = UnityEngine.Object.Instantiate(prefab, parent);
            viewObject.name = $"NotEnoughFor{skill.Name}";
            view = new View(viewObject);
            foreach(var statsData in skill.ConsumedStats.Values)
            {
                view.Text += string.Format(TEXT_TEMPLATE, AdjustedName($"{statsData.Name}"));
                view.Text += "\n";
            }
            view.Text = view.Text.Remove(view.Text.Length - 1);
        }

        private string AdjustedName(string statsName){
            string removedText = "Current";
            if(!statsName.Contains(removedText)){ return statsName; }
            string adjusted = statsName.Split(removedText)
                                       .Aggregate((sum, next) => sum + next);
            return adjusted;
        }

        ~NotEnoughHint(){
            UnityEngine.Object.Destroy(view);
        }

        [Serializable]
        class View
        {   
            private const string TEXT_PATH = "Text";

            public static implicit operator GameObject(View view) => view.gameObject;
            private GameObject gameObject;
            private TextMeshProUGUI _text;
            public string Text{
                get => _text.text;
                set => _text.SetText(value);
            }

            public View(GameObject gameObject){
                this.gameObject = gameObject;
                _text = this.gameObject.transform
                                       .Find(TEXT_PATH)
                                       .GetComponent<TextMeshProUGUI>();
                Text = "";
            }
        }
    }
}
