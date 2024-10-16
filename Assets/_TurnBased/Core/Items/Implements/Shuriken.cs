using TurnBasedPractice.Character;
using TurnBasedPractice.Effects;
using UnityEngine;

namespace TurnBasedPractice.Items
{
    public class Shuriken : Item
    {
        public Shuriken(){ 
            _id          = 5;
            _name        = "Shuriken";
            _icon        = null;
            _cost        = 200;
            _description = "Throw the Shuriken, deal damage to 2 times of target's strength.";
            _type        = ItemName.Shuriken;
        }
        public Shuriken(ItemSO so) : this(){
            AssignDataFromSO(so);
        }

        public override void Use(Hero user, Hero target){
            int damage = Mathf.FloorToInt(target.Stats[StatsSystem.StatsName.Strength] * 2);
            target.TakeDamage(damage);
            target.OnHurt(user);

            user.Controller.GetSelected().ExecuteInfo = UseText(user, target);
            user.Controller.GetSelected().ExecuteInfo += "\n" + AttackText(target, damage);
            string bestowedText = EffectSystem.Bestow(new Poison(), user, target);
            user.Controller.GetSelected().ExecuteInfo += bestowedText;
        }

    }
}
