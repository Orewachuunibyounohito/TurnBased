using TurnBasedPractice.Character;
using TurnBasedPractice.Character.Controller;
using TurnBasedPractice.Items;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedPractice.BattleCore.Selection
{
    public class NonMonoInventoryItemAction : NonMonoButtonAction
    {
        public Item Item{ get; set; }
        
        public NonMonoInventoryItemAction(Button button, Item item, Hero user, params Hero[] targets) : base(button, user, targets){
            Item = item;
        }

        public override void DoAction(MonoBehaviour mono){
            User.Inventory.UseItem(Item, Targets);
            base.DoAction(mono);
        }
        
        public override void Interact(PlayerController playerController){
            TargetControlState targetControl = playerController;
            targetControl.SetAction(this);
        }
    }
}