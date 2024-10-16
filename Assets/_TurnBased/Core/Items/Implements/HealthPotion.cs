using TurnBasedPractice.Character;

namespace TurnBasedPractice.Items
{
    public class HealthPotion : Item
    {
        public HealthPotion(){
            _id          = 1;
            _name        = "Health Potion";
            _icon        = null;
            _cost        = 3;
            _description = "Heal 10 hp";
            _type        = ItemName.HealthPotion;
        }
        public HealthPotion(ItemSO so) : this(){
            AssignDataFromSO(so);
        }
        
        public override void Use(Hero user, Hero target){
            int actualValue =
                target.Stats[StatsSystem.StatsName.MaxHp] - target.Stats[StatsSystem.StatsName.CurrentHp];
            actualValue = actualValue > 10 ? 10 : actualValue;

            target.Stats[StatsSystem.StatsName.CurrentHp] = target.Stats[StatsSystem.StatsName.CurrentHp] + actualValue;
            target.OnHeal(user);

            user.Controller.GetSelected().ExecuteInfo = UseText(user, target);
            user.Controller.GetSelected().ExecuteInfo += "\n" + HealText(target, actualValue);
        }

        private string HealText(Hero target, int amount) => string.Format(healthRecoveryTemplate, target.Name, amount);

        public static Item GenerateItem(ItemSO itemData){
            if(itemData == null){ return new HealthPotion(); }
            return new HealthPotion(itemData);
        }

    }
}
