using TurnBasedPractice.Character;

namespace TurnBasedPractice.Items
{
    public class BreakPotion : Item
    {   
        public BreakPotion(){
            _id          = 3;
            _name        = "Break Potion";
            _icon        = null;
            _cost        = 30;
            _description = "Let target's defense 20 points down.";
            _type        = ItemName.BreakPotion;
        }
        public BreakPotion(ItemSO so) : this(){
            AssignDataFromSO(so);
        }

        public override void Use(Hero user, Hero target){
            user.Controller.GetSelected().ExecuteInfo = UseText(user, target);     
        }

    }
}
