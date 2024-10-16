using System;
using TurnBasedPractice.Character;
using TurnBasedPractice.Localization;
using UnityEngine;

namespace TurnBasedPractice.Items
{
    public class Item : IItem, IEquatable<Item>
    {
        protected long   _id;
        protected string _name;
        protected Sprite _icon;
        protected uint   _cost;
        protected string _description;
        protected ItemName _type;

        // protected static string itemUseTemplate => LocalizationSettings.GetCommonTemplate(CommonStringTemplate.ItemUse);
        // protected static string takeDamageTemplate => LocalizationSettings.GetCommonTemplate(CommonStringTemplate.TakeDamage);
        // protected static string healthRecoveryTemplate => LocalizationSettings.GetCommonTemplate(CommonStringTemplate.HealthRecovery);
        // protected static string manaRecoveryTemplate => LocalizationSettings.GetCommonTemplate(CommonStringTemplate.ManaRecovery);
        protected static string itemUseTemplate => LocalizationSettings.GetCommonString(CommonStringTemplate.ItemUse).GetLocalizedString();
        protected static string takeDamageTemplate => LocalizationSettings.GetCommonString(CommonStringTemplate.TakeDamage).GetLocalizedString();
        protected static string healthRecoveryTemplate => LocalizationSettings.GetCommonString(CommonStringTemplate.HealthRecovery).GetLocalizedString();
        protected static string manaRecoveryTemplate => LocalizationSettings.GetCommonString(CommonStringTemplate.ManaRecovery).GetLocalizedString();
        
        public long   Id          => _id;
        public string Name        => _name;
        public Sprite Icon        => _icon;
        public uint   Cost        => _cost;
        public string Description => _description;
        public ItemName Type      => _type;
        
        // private string localizedName => LocalizationSettings.GetItemName(_type);
        private string localizedName => LocalizationSettings.GetItemString(_type).GetLocalizedString();

        public Item(){}

        public Item(long id, string name, Sprite icon = null, uint cost = 0, string description = ""){
            _id          = id;
            _name        = name;
            _icon        = icon;
            _cost        = cost;
            _description = description;
        }

        public override int GetHashCode(){
            return _id.GetHashCode() ^ _name.GetHashCode();
        }

        public bool Equals(Item other){
            return other != null && _id == other._id;
        }

        public virtual void Use(Hero user, Hero target){
            // user.PlayerController.GetSelected().ExecuteInfo = UseText(user, target);
            Debug.Log($"{user.Name} use a {Name} in {target.Name}");
        }

        protected void AssignDataFromSO(ItemSO so){
            _icon = so.image;
            
            if(so.IsCustom){
                _name        = so.displayName;
                _cost        = so.cost;
                _description = so.description;
            }
        }

        protected virtual string UseText(Hero user, Hero target) => string.Format(itemUseTemplate, user.Name, localizedName, target.Name);
        protected virtual string AttackText(Hero target, int damage) => string.Format(takeDamageTemplate, target.Name, damage);
    }
}
