using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TurnBasedPractice.BattleCore.Loggers;
using TurnBasedPractice.BattleCore.Selection;
using TurnBasedPractice.Character.Controller;
using TurnBasedPractice.Effects;
using TurnBasedPractice.InfoSystem;
using TurnBasedPractice.InventorySystem;
using TurnBasedPractice.SkillSystem;
using TurnBasedPractice.SO;
using TurnBasedPractice.StatsSystem;
using UnityEngine;


namespace TurnBasedPractice.Character
{
    public class Hero : MonoBehaviour, IDamageable
    {
        [ShowInInspector]
        private HashSet<Skill>  _ownedSkills = new HashSet<Skill>();
        private Inventory       _inventory;
        [ShowInInspector]
        private Stats           _stats       = new Stats();
        [SerializeField]
        private StatsSO         _statsSO;
        private HpBarView _hpBar/* { get; private set; } */;


        public string      Name{ get; set; }
        public Stats       Stats{ get => _stats; }
        public Hero[]      Targets { get; private set; }
        public List<Skill> SkillInSlot{ get => _ownedSkills.ToList(); }
        public Inventory   Inventory{ get => _inventory; }
        [ShowInInspector, ReadOnly]
        public Buff        Buff{ get; private set; }
        [ShowInInspector]
        public IHeroController Controller;
        [ShowInInspector]
        public bool IsReady{ get; set; }
        [ShowInInspector]
        public int  CurrentFocus{ get; set; } = 0;
        [ShowInInspector]
        public float ActionPriority{ get; set; }
        public IFocusable Focusable{ get; set; }
        public bool IsConstraint{ get; set; }
        public bool IsAsleep{ get; set; }

        [SerializeField]
        private bool isNPC;

        public event Action Hurt;
        public event Action<Hero> Die;

        public static implicit operator string(Hero hero) => hero.Name;

        private void Awake()
        {}

        public void Init(StatsSO statsSO)
        {
            foreach (var statData in statsSO.RequiredStats){
                _stats.Add(statData);
            }
            
            if(!_stats.ContainsKey(StatsName.CurrentHp)){ _stats.Add(StatsName.CurrentHp, _stats[StatsName.MaxHp]); }
            _stats[StatsName.CurrentHp] = _stats[StatsName.MaxHp];
            if(!_stats.ContainsKey(StatsName.CurrentMp)){ _stats.Add(StatsName.CurrentMp, _stats[StatsName.MaxMp]); }
            _stats[StatsName.CurrentMp] = _stats[StatsName.MaxMp];
            Focusable = new HeroFocusable(this);
            Buff      = new Buff(this);
        }

        public bool LearnSkill(Skill skill){
            return _ownedSkills.Add(skill);
        }

        public bool ContainSkill(SkillName skillName) => _ownedSkills.Contains(SkillFactory.GenerateSkill(skillName));

        public void PhaseOK() => IsReady = true;

        public void UpdatePriority(){
            if(_stats.ContainsKey(StatsName.Agility)){
                ActionPriority = _stats[StatsName.Agility];
            }else{
                ActionPriority = 0;
            }
        }

        public void SetTargets(params Hero[] targets){ Targets = targets; }
        public void SetHpBar(HpBarView hpBar) => _hpBar = hpBar;

        public void SetDefaultInventory() => _inventory = Inventory.DefaultInventory(this);

        public void TakeDamage(int damage){
            HandleWakeUp(damage);
            _stats[StatsName.CurrentHp] =
                Math.Clamp(_stats[StatsName.CurrentHp] - damage, 0, _stats[StatsName.MaxHp]);
        }

        public void OnHeal(Hero healer){
            _hpBar.UpdateBar((float)_stats[StatsName.CurrentHp] / _stats[StatsName.MaxHp]);
        }

        public void OnHurt(Hero attacker){
            _hpBar.UpdateBar((float)_stats[StatsName.CurrentHp] / _stats[StatsName.MaxHp]);
            Hurt?.Invoke();

            if(_stats[StatsName.CurrentHp] == 0){
                Die?.Invoke(attacker);
            }
        }

        private void HandleWakeUp(int damage){
            if(damage == 0){ return ; }

        }

        public void Performing(bool isBattleOver){
            StartCoroutine(PerformingTask(isBattleOver));
        }

        private IEnumerator PerformingTask(bool isBattleOver){
            LoggerSystem.Add($"-- PerformTask -- [{DateTime.Now.ToLocalTime()}]");
            LoggerSystem.Add($"\t{Name} do {Controller.GetSelected()}");
            if(isBattleOver){
                Controller.GetSelected().Finish();
                PhaseOK();
                yield break;
            }

            LoggerSystem.Add($"\t{Name} PreProcess start");
            Buff.PreProcessEffect();
            while(BattleInfoSystem.HasInfo() && !BattleInfoSystem.IsFinish){
                yield return null;
            }
            LoggerSystem.Add($"\t{Name} PreProcess end.");
            
            LoggerSystem.Add($"\t{Name} DoAction start.");
            if(IsConstraint || IsAsleep){
                Controller.GetSelected().Finish();
            }else{
                Controller.GetSelected().DoAction(this);
            }
            while(!Controller.GetSelected().IsFinish){
                yield return null;
            }
            LoggerSystem.Add($"\t{Name} DoAction end.");
            
            LoggerSystem.Add($"\t{Name} PostProcess start.");
            Buff.PostProcessEffect();
            while(BattleInfoSystem.HasInfo() && !BattleInfoSystem.IsFinish){
                yield return null;
            }
            LoggerSystem.Add($"\t{Name} PostProcess end.");
            
            IsConstraint = false;
            PhaseOK();
            LoggerSystem.Add($"\t{Name} PhaseOK.");
        }
    }
}
