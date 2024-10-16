using System;
using TurnBasedPractice.Character.Controller;
using UnityEngine.UI;

namespace TurnBasedPractice.BattleCore.Selection
{
    public class NonMonoContainerObject : IFocusable
    {
        public ControlStateType selfLayer, childrenLayer;
        public Button Button{ get; private set; }
        private PlayerController _playerController;

        public NonMonoContainerObject(Button button, ControlStateType selfLayer, ControlStateType childrenLayer){
            Button             = button;
            this.selfLayer     = selfLayer;
            this.childrenLayer = childrenLayer;
        }
        public NonMonoContainerObject(Button button, ControlStateType selfLayer, ControlStateType childrenLayer, PlayerController playerController) : this(button, selfLayer, childrenLayer){
            _playerController = playerController;
            Button?.onClick.AddListener(OnFocusClick);
        }

        public void Interact(PlayerController playerController){
            playerController.stateMachine.ChangeState(playerController.ControlStates[childrenLayer]);
        }
        public void Interact(){
            ControlState currentState = _playerController.stateMachine.CurrentState as ControlState;
            if(currentState.IsOtherLayer(selfLayer)){
                _playerController.stateMachine.ChangeState(_playerController.ControlStates[selfLayer]);
                return ;
            }
            _playerController.stateMachine.ChangeState(_playerController.ControlStates[childrenLayer]);
        }

        public void OnFocusEnter(){
            Button?.OnPointerEnter(default);
        }
        
        public void OnFocusClick(){
            // Button.OnPointerClick(default);
            if(!_playerController.IsSelectingPhase()){ return ; }
            Interact();
        }

        public void OnFocusExit(){
            Button?.OnPointerExit(default);
        }
    }
}