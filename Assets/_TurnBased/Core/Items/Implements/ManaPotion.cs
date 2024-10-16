using System;
using TurnBasedPractice.Character;

namespace TurnBasedPractice.Items
{
    public class ManaPotion : Item
    {
        public ManaPotion(){
            _id          = 2;
            _name        = "Mana Potion";
            _icon        = null;
            _cost        = 5;
            _description = "Recover 10 mp";
            _type        = ItemName.ManaPotion;
        }
        public ManaPotion(ItemSO so) : this(){
            AssignDataFromSO(so);
        }

        public override void Use(Hero user, Hero target){
            target.Stats[StatsSystem.StatsName.CurrentMp] = Math.Clamp(
                target.Stats[StatsSystem.StatsName.CurrentMp] + 10,
                target.Stats[StatsSystem.StatsName.CurrentMp],
                target.Stats[StatsSystem.StatsName.MaxMp]
            );
            int actualValue =
                target.Stats[StatsSystem.StatsName.MaxMp] - target.Stats[StatsSystem.StatsName.CurrentMp];
            actualValue = actualValue > 10 ? 10 : actualValue;
            user.Controller.GetSelected().ExecuteInfo = UseText(user, target);
            user.Controller.GetSelected().ExecuteInfo += "\n" + ManaRecoveryText(target, actualValue);
        }

        private string ManaRecoveryText(Hero target, int amount) => string.Format(manaRecoveryTemplate, target.Name, amount);
    }
}
