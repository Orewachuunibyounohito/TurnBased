using System;
using Sirenix.OdinInspector;
using TurnBasedPractice.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemView : MonoBehaviour
{
    [ShowInInspector]
    public Image                 Icon{ get; private set; }
    [ShowInInspector]
    public TMPro.TextMeshProUGUI DescriptionText{ get; private set; }
    [ShowInInspector]
    public TMPro.TextMeshProUGUI AmountText{ get; private set; }

    public InventoryItemView Init(Inventory.Slot slot){
        Icon.sprite = slot.item.Icon;
        DescriptionText.SetText( slot.item.Name );
        AmountText.SetText( $"x{slot.amount}" );
        return this;
    }

    private void Awake(){
        Icon            = transform.Find("Icon").GetComponent<Image>();
        DescriptionText = transform.Find("Description").GetComponent<TMPro.TextMeshProUGUI>();
        AmountText      = transform.Find("Amount").GetComponent<TMPro.TextMeshProUGUI>();
    }
}
