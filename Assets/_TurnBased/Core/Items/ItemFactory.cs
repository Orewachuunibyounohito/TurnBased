using System.Collections.Generic;
using TurnBasedPractice.Items;
using TurnBasedPractice.Resource;
using UnityEngine;

public static class ItemFactory
{
    private const string ITEM_SETTINGS_PATH = "DefaultItemSO/DefaultItemSettings";

    private static Dictionary<ItemName, Item> itemDictionary;

    static ItemFactory(){
        var itemSettings = Resources.Load<ItemSettingsSO>(SoPath.ITEM_SETTINGS_PATH);

        itemDictionary = new Dictionary<ItemName, Item>(){
            { ItemName.HealthPotion, new HealthPotion(itemSettings.itemSoList.Find((so) => so.item == ItemName.HealthPotion)) },
            { ItemName.ManaPotion,   new ManaPotion(itemSettings.itemSoList.Find((so)   => so.item == ItemName.ManaPotion)) },
            { ItemName.BreakPotion,  new BreakPotion(itemSettings.itemSoList.Find((so)  => so.item == ItemName.BreakPotion)) },
            { ItemName.SleepPotion,  new SleepPotion(itemSettings.itemSoList.Find((so)  => so.item == ItemName.SleepPotion)) },
            { ItemName.Shuriken,     new Shuriken(itemSettings.itemSoList.Find((so)     => so.item == ItemName.Shuriken)) },
            { ItemName.Panacea,      new Panacea(itemSettings.itemSoList.Find((so)      => so.item == ItemName.Panacea)) }
        };
    }

    public static Item GenerateItem(ItemName itemName) => itemDictionary[itemName];
}
