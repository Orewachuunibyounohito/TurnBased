using System;
using System.Collections.Generic;
using TurnBasedPractice.Character;
using TurnBasedPractice.Items;

namespace TurnBasedPractice.InventorySystem
{
    public class Inventory
    {
        public HashSet<Slot> Slots { get; private set; }
        public Hero Owner { get; private set; }

        public Inventory(Hero owner)
        {
            Slots = new HashSet<Slot>();
            Owner = owner;
        }

        public void AddItem(Item item, uint amount = 1)
        {
            Slot slot = new Slot(item, amount);
            if (Slots.Add(slot))
            {
                slot.ItemUseUp += RemoveSlot(item);
            }
        }
        public void UseItem(Item item, params Hero[] targets)
        {
            if (Slots.TryGetValue(new Slot(item, 0), out Slot slot))
            {
                slot.UseItem(Owner, targets);
            }
        }
        public void RemoveItem(Item item){
            Slots.RemoveWhere((slot) => slot.item.Name == item.Name);
        }
        public Action RemoveSlot(Item item) => () => RemoveItem(item);

        public static Inventory DefaultInventory(Hero hero)
        {
            var inventory = new Inventory(hero);
            inventory.AddItem(ItemFactory.GenerateItem(ItemName.HealthPotion), 3);
            inventory.AddItem(ItemFactory.GenerateItem(ItemName.ManaPotion), 10);
            inventory.AddItem(ItemFactory.GenerateItem(ItemName.BreakPotion), 5);
            inventory.AddItem(ItemFactory.GenerateItem(ItemName.SleepPotion), 1);
            inventory.AddItem(ItemFactory.GenerateItem(ItemName.Shuriken), 20);
            inventory.AddItem(ItemFactory.GenerateItem(ItemName.Panacea), 20);
            return inventory;
        }

        public class Slot : IEquatable<Slot>
        {
            public Item item;
            public uint amount;

            public event Action ItemUsed, ItemUseUp;

            public Slot(Item item, uint amount)
            {
                this.item = item;
                this.amount = amount;
            }

            public void UseItem(Hero user, params Hero[] targets)
            {
                foreach (var target in targets)
                {
                    item.Use(user, target);
                }
                amount--;
                ItemUsed?.Invoke();

                bool isUseUp = amount == 0;
                if (isUseUp)
                {
                    ItemUseUp?.Invoke();
                }
            }

            public override bool Equals(object obj)
            {
                return obj is Slot && Equals(obj as Slot);
            }

            public override int GetHashCode()
            {
                return item.GetHashCode();
            }

            public bool Equals(Slot other)
            {
                return other != null && item.Equals(other.item);
            }

        }
    }
}
