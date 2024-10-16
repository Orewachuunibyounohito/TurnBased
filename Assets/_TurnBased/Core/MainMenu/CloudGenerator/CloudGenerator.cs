using ChuuniExtension.CountdownTool;
using TurnBasedPractice.Animates;
using TurnBasedPractice.Resource;
using UnityEngine;

namespace TurnBasedPractice.MainMenu
{
    public class CloudGenerator : MonoBehaviour
    {
        private const int LEFT_BOUND = -9;
        private const int RIGHT_BOUND = 9;
        private static GameObject cloudPrefab;

        [SerializeField]
        private Transform collection;
        [SerializeField]
        private CloudGeneratorSettings settings;
        private ICountdown countdown;
        private int amount = 0;
        private bool isEnd;

        private void Awake(){
            cloudPrefab = Resources.Load<PrefabsSettings>(SoPath.PREFABS_SETTINGS_PATH)
                                   .Cloud;
        }

        private void Start(){
            countdown = CountdownFactory.Generate(settings.CountdownType,
                                                  new CountdownConfig(settings.Speed, settings.Distance , settings.Interval));
        }

        private void Update(){
            if(isEnd){ return ; }
            countdown.Update();
            if(!countdown.TimesUp){ return ; }
            countdown.Reset();
            Generate();
            isEnd = amount == settings.Amount;
        }

        private void Generate(){
            GameObject cloud = Instantiate(cloudPrefab, collection);
            cloud.transform.position += (Vector2.one * Random.Range(-settings.Deviation, settings.Deviation)).AsVector3();
            cloud.transform.localScale *= 1 - Random.Range(-settings.ScaleRandomFactor, settings.ScaleRandomFactor);
            MoveLoopWithBoundsAnimate moveAnimate = cloud.AddComponent<MoveLoopWithBoundsAnimate>();
            moveAnimate.Speed = settings.Speed;
            moveAnimate.Bounds = new Animates.Bounds{ Left = LEFT_BOUND, Right = RIGHT_BOUND };
            moveAnimate.Execute();
            amount++;
        }
    }
}
