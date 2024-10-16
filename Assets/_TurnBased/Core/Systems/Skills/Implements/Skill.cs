using System;
using TurnBasedPractice.Character;
using TurnBasedPractice.Localization;
using TurnBasedPractice.SkillSystem;
using TurnBasedPractice.StatsSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Localization;

[Serializable]
public abstract class Skill : ISkill, IConsumption, IEquatable<Skill>
{
    protected static string useTemplate{ get; private set; }
    protected static string takeDamageTemplate{ get; private set; }

    public virtual SkillName skillName{ get; }

    public string Name { get; set; }
    public string Description { get; set; }
    public float  ExecuteTime{ get; set; }
    public Stats  ConsumedStats { get; set; }

    public event Action skillUsed;
    public event Action<Skill> NotEnoughToUse;

    static Skill(){
        LocalizedString useTemplate = LocalizationSettings.GetCommonString(CommonStringTemplate.SkillUse);
        LocalizedString takeDamageTemplate = LocalizationSettings.GetCommonString(CommonStringTemplate.TakeDamage);
        useTemplate.StringChanged += (translatedString) => Skill.useTemplate = translatedString;
        takeDamageTemplate.StringChanged += (translatedString) => Skill.takeDamageTemplate = translatedString;
    }

    public Skill(){
        Name = $"{skillName}";
        LocalizationSettings.GetSkillString(skillName).StringChanged += UpdateString;
        ConsumedStats = new Stats();
    }

    public virtual void Do(Hero user, params Hero[] targets){
        skillUsed?.Invoke();
    }

    public virtual bool CanUse(Hero user){
        bool canUse = true;
        foreach(var consumedData in ConsumedStats.Values){
            bool statsNotFound = !user.Stats.TryGetValue(consumedData.Name, out Stats.Data userData);
            if(statsNotFound){ canUse = false; break; }
            bool notEnough = userData.Value < consumedData.Value;
            if(notEnough){ canUse = false; break; }
        }
        if(!canUse){
            NotEnoughToUse?.Invoke(this);
            return false;
        }
        return true;
    }

    public virtual int HandleGuard(int damage, Hero sufferer){
        if(sufferer.Stats.ContainsKey(StatsName.Guard)){
            damage /= 2;
        }
        return damage;
    }
    
    protected virtual string UseText(Hero user) => string.Format(useTemplate, user.Name, Name);
    protected virtual string AttackText(Hero target, int damage) => string.Format(takeDamageTemplate, target.Name, damage);
    protected virtual void UpdateString(string translatedString) => Name = translatedString;

    public override int GetHashCode() => Name.GetHashCode();

    public bool Equals(Skill other){
        if(other == null){ return false; }
        
        return Name.Equals(other.Name);
    }
}
