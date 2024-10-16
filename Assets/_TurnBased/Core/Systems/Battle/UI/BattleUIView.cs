using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System;

public class BattleUIView : MonoBehaviour
{
    [SerializeField]
    private GameObject itemActionPrefab;
    private List<TMPro.TextMeshProUGUI> skillTexts;

    [ShowInInspector]
    public Transform ActionPanel{ get; private set; }
    [ShowInInspector]
    public Transform SkillPanel{ get; private set; }
    [ShowInInspector]
    public List<TMPro.TextMeshProUGUI> SkillTexts => skillTexts;
    [ShowInInspector]
    public Transform InventoryPanel{ get; private set; }
    [ShowInInspector]
    public RectTransform InventoryContent{ get; private set; }
    [ShowInInspector]
    public Transform             InfoPanel{ get; private set; }
    public TMPro.TextMeshProUGUI InfoText{ get; private set; }
    public PlayerStatusView P1StatusPanel{ get; private set; }
    public PlayerStatusView P2StatusPanel{ get; private set; }

    public GameObject ItemActionPrefab => itemActionPrefab;

    public float ItemHeight => InventoryContent.GetComponent<GridLayoutGroup>().cellSize.y;
    public float ViewPortHeight => InventoryContent.parent.GetComponent<RectTransform>().rect.height;
    public float PageHeight => ItemHeight * (float)Math.Floor(ViewPortHeight / ItemHeight);
    
    private void Awake(){
    }

    public void Init(){
        CachePanel();
        CacheSkillTexts();
        CacheInfoText();
        CacheInventoryContent();
    }


    private void CachePanel(){
        ActionPanel    = transform.Find("ActionPanel");
        SkillPanel     = transform.Find("SkillPanel");
        InventoryPanel = transform.Find("InventoryPanel");
        InfoPanel      = transform.Find("BattleInfoPanel");
        P1StatusPanel  = transform.Find("P1StatusPanel").gameObject.AddComponent<PlayerStatusView>();
        P2StatusPanel  = transform.Find("P2StatusPanel").gameObject.AddComponent<PlayerStatusView>();
    }

    private void CacheSkillTexts(){
        skillTexts = new List<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI skillText;
        foreach (var skillBtn in SkillPanel.GetComponentsInChildren<Button>()){
            skillText = skillBtn.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            skillTexts.Add(skillText);
        }
    }

    private void CacheInfoText(){
        InfoText = InfoPanel.GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    private void CacheInventoryContent(){
        InventoryContent = InventoryPanel.GetComponentInChildren<ScrollRect>().content;
        var viewRect = InventoryContent.parent.parent.GetComponent<RectTransform>().rect;
        InventoryContent.GetComponent<GridLayoutGroup>().cellSize = 
            new Vector2(viewRect.width - 10, InventoryContent.rect.height / 4 - 10);
    }

    public void SkillPanelSwitch() =>
        SkillPanel.gameObject.SetActive(!SkillPanel.gameObject.activeInHierarchy);

    public void InfoPanelSwitch() =>
        InfoPanel.gameObject.SetActive(!InfoPanel.gameObject.activeInHierarchy);

    public void InventoryPanelSwitch() =>
        InventoryPanel.gameObject.SetActive(!InventoryPanel.gameObject.activeInHierarchy);
}
