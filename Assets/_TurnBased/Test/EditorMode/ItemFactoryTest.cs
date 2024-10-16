using System;
using System.Threading;
using NUnit.Framework;
using TurnBasedPractice.Items;
using UnityEngine;

public class ItemFactoryTest
{
    [Test]
    [TestCase(ItemName.HealthPotion, typeof(HealthPotion))]
    [TestCase(ItemName.SleepPotion, typeof(SleepPotion))]
    public void ItemFactoryGenerateItemWithItemName(ItemName itemName, Type expected){
        var item = ItemFactory.GenerateItem(itemName);

        Assert.AreEqual(expected, item.GetType());
    }
}
