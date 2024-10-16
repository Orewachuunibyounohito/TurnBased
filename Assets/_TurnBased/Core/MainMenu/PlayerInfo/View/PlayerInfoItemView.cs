using TMPro;
using TurnBasedPractice.Resource;
using UnityEngine;

namespace TurnBasedPractice.MainMenu
{
    public class PlayerInfoItemView
    {
        private readonly static GameObject playerInfoItemTemplate;
        static PlayerInfoItemView(){
            playerInfoItemTemplate = Resources.Load<PrefabsSettings>(SoPath.PREFABS_SETTINGS_PATH)
                                              .PlayerInfoItemTemplate;
        }

        private Transform view;
        public TextMeshProUGUI LabelText;
        public TextMeshProUGUI ValueText;

        #if UNITY_EDITOR
        public string name => view.name;
        public GameObject gameObject => view.gameObject;
        #endif

        private PlayerInfoItemView(){
            view = Object.Instantiate(playerInfoItemTemplate).transform;
            LabelText = view.Find("Label/Text").GetComponent<TextMeshProUGUI>();
            ValueText = view.Find("Value/Text").GetComponent<TextMeshProUGUI>();
        }

        public void SetParant(Transform container) => view.SetParent(container);
        public override string ToString() => $"PlayerInfoItemView:[LabelText=\"{LabelText.text}\", ValueText=\"{ValueText.text}\"]";

        public static PlayerInfoItemView Generate(string label, string value){
            var view = new PlayerInfoItemView();
            view.LabelText.SetText(label);
            view.ValueText.SetText(value);
            return view;
        }
    }
}