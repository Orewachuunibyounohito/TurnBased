using System;
using System.Collections;
using Sirenix.OdinInspector;
using TurnBasedPractice.Character;
using TurnBasedPractice.Character.Controller;
using TurnBasedPractice.InfoSystem;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedPractice.BattleCore.Selection
{
    public abstract class NonMonoButtonAction : ICustomAction, IFocusable
    {
        [ShowInInspector]
        public virtual Hero   User{ get; set; }
        [ShowInInspector]
        public virtual Hero[] Targets{ get; set; }
        public virtual Button Button{ get; private set; }

        public virtual float ExecuteTime{ get; set; } = 0;
        public string ExecuteInfo{
            get => _executeInfo;
            set => _executeInfo = value;
        }
        private string _executeInfo;

        [ShowInInspector]
        public virtual bool  IsFinish{ get; private set; } = false;

        private PlayerController _playerController;

        public NonMonoButtonAction(Button button, Hero user, params Hero[] targets){
            Button  = button;
            User    = user;
            Targets = targets;
            _playerController = user.Controller as PlayerController;
            Button?.onClick.AddListener(OnFocusClick);
        }

        public virtual void Interact(PlayerController playerController){}
        public virtual void Interact(){
            ControlState currentState = _playerController.stateMachine.CurrentState as ControlState;
            if(currentState.IsOtherLayer(this)){
                _playerController.stateMachine.ChangeState(_playerController.ControlStates[ControlStateType.Normal]);
                return ;
            }
            currentState.ChangingFocus(this);
            currentState.DecisionForMobile();
        }

        public virtual void OnFocusEnter(){
            Button?.OnPointerEnter(default);
        }

        public virtual void OnFocusClick(){
            if(!_playerController.IsSelectingPhase()){ return ; }
            Interact();
        }

        public virtual void OnFocusExit(){
            Button?.OnPointerExit(default);
        }


        public virtual void DoAction(MonoBehaviour mono){
            BattleInfoSystem.Clear();
            BattleInfoSystem.Add(ExecuteInfo);
            BattleInfoSystem.Play();
            ExecuteInfo = "";
            
            mono.StartCoroutine(ExecuteTask());
        }
        
        public virtual void Reset() => IsFinish = false;

        public virtual void Finish(){
            IsFinish = true;
        }

        private IEnumerator ExecuteTask(){
            if(ExecuteTime == 0){ yield return null; }
            else                { yield return new WaitForSeconds(ExecuteTime); }
            while(!BattleInfoSystem.IsFinish){
                yield return null;
            }
            Finish();
        }
    }
}