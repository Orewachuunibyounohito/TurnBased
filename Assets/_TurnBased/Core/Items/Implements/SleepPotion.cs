using TurnBasedPractice.Character;
using TurnBasedPractice.Effects;

namespace TurnBasedPractice.Items
{
    public class SleepPotion : Item
    {
        public SleepPotion(){ 
            _id          = 4;
            _name        = "Sleep Potion";
            _icon        = null;
            _cost        = 100;
            _description = "Take a asleep status on target.";
            _type        = ItemName.SleepPotion;
        }
        public SleepPotion(ItemSO so) : this(){
            AssignDataFromSO(so);
        }

        public override void Use(Hero user, Hero target){
            var asleep = EffectFactory.Generate(EffectName.Asleep);
            var bestowFactor = new Factor{
                value = 100,
                percent = 0,
                multiplier = 1
            };

            user.Controller.GetSelected().ExecuteInfo = UseText(user, target);
            string bestowedText = EffectSystem.Bestow(asleep, user, target, bestowFactor);
            user.Controller.GetSelected().ExecuteInfo += bestowedText;
        }

    }
}
