using System.Collections.Generic;
using TurnBasedPractice.Character;
using TurnBasedPractice.Effects;
using TurnBasedPractice.Items;

public class Panacea : Item
{
    public Panacea(){ 
        _id          = 6;
        _name        = "Panacea";
        _icon        = null;
        _cost        = 200;
        _description = "Dispel all of debuff with target.";
        _type        = ItemName.Panacea;
    }
    public Panacea(ItemSO so) : this(){
        AssignDataFromSO(so);
    }

    public override void Use(Hero user, Hero target){
        user.Controller.GetSelected().ExecuteInfo = UseText(user, target);

        List<IEffect> debuffs = target.Buff.GetNegativeBuffs();
        foreach(var debuff in debuffs){
            target.Buff.ShortenBuff(debuff.Name, 99);
            user.Controller.GetSelected().ExecuteInfo += "\n"+debuff.LiftText(target);
        }
    }
}
