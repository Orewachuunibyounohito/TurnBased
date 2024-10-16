using System;
using TurnBasedPractice.BattleCore.Selection;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TurnBasedPractice.Character.Controller
{
    public class NonPlayerController : IHeroController
    {
        private Hero _hero, _opposition;
        private GameObject _autoActionObj;

        public ICustomAction selectedAction;

        public NonPlayerController(Hero hero, Hero opposition){
            _hero = hero;
            _opposition = opposition;
            
            _autoActionObj = new GameObject("AutoAction");
            _autoActionObj.transform.SetParent(_hero.transform);
        }

        public void Selecting(){
            selectedAction = SelectActionWithRandom();
            if(selectedAction != default){ _hero.PhaseOK(); }
        }

        public void          ActionFinished()           => selectedAction.Finish();
        public ICustomAction GetSelected()              => selectedAction;
        public void          PlayerDie(Hero attacker)   => selectedAction = new DeathAction(_hero, attacker);
        public void          PlayerEscaped()            => selectedAction = new EscapedAction(_hero);

        private ICustomAction SelectActionWithRandom(){
            _hero.UpdatePriority();
            selectedAction = default;
            var firstIndex = Random.Range(0, Math.Clamp(_hero.SkillInSlot.Count, 1, 4));
            var tempIndex  = firstIndex;
            ICustomAction action = default;
            while(action == default){
                if(_hero.SkillInSlot[tempIndex].CanUse(_hero)){
                    SelectionData selectionData = SelectionData.SkillSelectionData(null, _hero.SkillInSlot[tempIndex], _hero, _hero.Targets);
                    action = SelectionFactory.GenerateSkillSelection(selectionData);
                    break;
                }
                tempIndex++;
                tempIndex = tempIndex == _hero.SkillInSlot.Count? 0 : tempIndex;
                if(tempIndex == firstIndex){
                    SelectionData selectionData = SelectionData.DefenseSelectionData(null, _hero, _hero.Targets);
                    action = SelectionFactory.GenerateDefenseSelection(selectionData);
                    _hero.ActionPriority += 3;
                }
            }
            action.User    = _hero;
            action.Targets = _hero.Targets;
            
            return action;
        }
    }
}
