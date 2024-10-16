using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turn-based/Item/Item Settings", fileName = "New Item Settings")]
public class ItemSettingsSO : ScriptableObject
{
    public List<ItemSO> itemSoList = new List<ItemSO>();
}
