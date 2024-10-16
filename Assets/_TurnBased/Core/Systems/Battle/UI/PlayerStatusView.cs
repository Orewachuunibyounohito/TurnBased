using UnityEngine;

public class PlayerStatusView : MonoBehaviour
{
    public TMPro.TextMeshProUGUI NameText{ get; private set; }
    public TMPro.TextMeshProUGUI LevelText{ get; private set; }
    public HpBarView HpBar{ get; private set; }

    private void Awake(){
        NameText  = transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>();
        LevelText = transform.Find("Level/Number").GetComponent<TMPro.TextMeshProUGUI>();
        HpBar     = transform.Find("Hp").gameObject.AddComponent<HpBarView>();
    }

    public void UpdateNameText(string name) => NameText.SetText(name);
    public void UpdateLevelText(int level)  => LevelText.SetText($"{level}");
    public void UpdateHpBar(float percent)  => HpBar.UpdateBar(percent);
}
