using System;
using TurnBasedPractice.Character;
using TurnBasedPractice.Character.Controller;
using TurnBasedPractice.Items;
using UnityEngine.UI;

namespace TurnBasedPractice.BattleCore.Selection
{
    [Serializable]
    public class SelectionData
    {
        public Hero             User{ get; set; }
        public Hero[]           Targets{ get; set; }
        public Button           Button{ get; set; }
        public Skill            Skill{ get; set; }
        public Item             Item{ get; set; }
        public ControlStateType SelfLayer{ get; set; }
        public ControlStateType ChildLayer{ get; set; }

        SelectionData(){
            User = default;
            Targets = default;
            Button = default;
            Skill = default;
            Item = default;
            SelfLayer = default;
            ChildLayer = default;
        }

        public class Builder
        {
            private SelectionData build;

            public Builder() => build = new SelectionData();

            public Builder User(Hero user){
                build.User = user;
                return this;
            }
            public Builder Targets(params Hero[] targets){
                build.Targets = targets;
                return this;
            }
            public Builder Button(Button button){
                build.Button = button;
                return this;
            }
            public Builder Skill(Skill skill){
                build.Skill = skill;
                return this;
            }
            public Builder Item(Item item){
                build.Item = item;
                return this;
            }
            public Builder SelfLayer(ControlStateType selfLayer){
                build.SelfLayer = selfLayer;
                return this;
            }
            public Builder ChildLayer(ControlStateType childLayer){
                build.ChildLayer = childLayer;
                return this;
            }
            public SelectionData Build(){ return build; }
        }

        public static SelectionData NormalSelectionData(Button button, ControlStateType selfLayer, ControlStateType childLayer){
            return new Builder().Button(button)
                                .SelfLayer(selfLayer)
                                .ChildLayer(childLayer)
                                .Build();
        }
        public static SelectionData AttackSelectionData(Button button, Hero user){
            return new Builder().Button(button)
                                .User(user)
                                .SelfLayer(ControlStateType.Normal)
                                .ChildLayer(ControlStateType.Attack)
                                .Build();
        }
        public static SelectionData InventorySelectionData(Button button, Hero user){
            return new Builder().Button(button)
                                .User(user)
                                .SelfLayer(ControlStateType.Normal)
                                .ChildLayer(ControlStateType.Inventory)
                                .Build();
        }
        public static SelectionData DefenseSelectionData(Button button, Hero user, params Hero[] targets){
            return new Builder().Button(button)
                                .User(user)
                                .Targets(targets)
                                .Build();
        }
        public static SelectionData EscapeSelectionData(Button button, Hero user, params Hero[] targets){
            return new Builder().Button(button)
                                .User(user)
                                .Targets(targets)
                                .Build();
        }
        public static SelectionData SkillSelectionData(Button button, Skill skill, Hero user, params Hero[] targets){
            return new Builder().Button(button)
                                .Skill(skill)
                                .User(user)
                                .Targets(targets)
                                .Build();
        }
        public static SelectionData ItemSelectionData(Button button, Item item, Hero user, params Hero[] targets){
            return new Builder().Button(button)
                                .Item(item)
                                .User(user)
                                .Targets(targets)
                                .Build();
        }
        public static SelectionData TargetSelectionData(Hero user){
            return new Builder().User(user)
                                .Build();
        }
    }
}