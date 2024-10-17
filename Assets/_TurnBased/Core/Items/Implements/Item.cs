using System;
using TurnBasedPractice.Character;
using TurnBasedPractice.Localization;
using UnityEngine;
using UnityEngine.Localization;

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

        protected static string itemUseTemplate;
        protected static string takeDamageTemplate;
        protected static string healthRecoveryTemplate;
        protected static string manaRecoveryTemplate;
        
        public long   Id          => _id;
        public string Name        => _name;
        public Sprite Icon        => _icon;
        public uint   Cost        => _cost;
        public string Description => _description;
        public ItemName Type      => _type;
        
        private string localizedName = "None";

        static Item(){
            LocalizationSettings.GetCommonString(CommonStringTemplate.ItemUse).StringChanged += UpdateItemUseTemplate;
            LocalizationSettings.GetCommonString(CommonStringTemplate.TakeDamage).StringChanged += UpdateTakeDamageTemplate;
            LocalizationSettings.GetCommonString(CommonStringTemplate.HealthRecovery).StringChanged += UpdateHealthRecoveryTemplate;
            LocalizationSettings.GetCommonString(CommonStringTemplate.ManaRecovery).StringChanged += UpdateManaRecoveryTemplate;
        }

        public Item(){}

        public Item(long id, string name, Sprite icon = null, uint cost = 0, string description = "") : this(){
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

        public void RegisterStringChanged() => LocalizationSettings.GetItemString(_type).StringChanged += UpdateLocalizedName;
        private void UpdateLocalizedName(string value) => localizedName = value;

        private static void UpdateItemUseTemplate(string value) => itemUseTemplate = value;
        private static void UpdateTakeDamageTemplate(string value) => takeDamageTemplate = value;
        private static void UpdateHealthRecoveryTemplate(string value) => healthRecoveryTemplate = value;
        private static void UpdateManaRecoveryTemplate(string value) => manaRecoveryTemplate = value;        
    }
}
