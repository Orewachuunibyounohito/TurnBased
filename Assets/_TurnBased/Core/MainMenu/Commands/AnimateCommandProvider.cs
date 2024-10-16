using TurnBasedPractice.Animates;
using UnityEngine;

public class AnimateCommandProvider : MonoBehaviour
{
    public void AddIrisIn(GameObject gameObject){
        gameObject.AddComponent<IrisIn>();
    }

    public void AddIrisIn_Ui(GameObject gameObject){
        gameObject.AddComponent<IrisIn_UI>();
    }
}
