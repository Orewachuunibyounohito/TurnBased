using System;
using System.Collections.Generic;
using TurnBasedPractice.BattleCore;
using TurnBasedPractice.BattleCore.Loggers;
using TurnBasedPractice.BattleCore.Selection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TurnBasedPractice.Character.Controller
{
    public class ControlState : IState
    {
        public static IFocusable   FocusObject{ get; protected set; }

        protected Hero             player;
        protected PlayerController controller;
        protected StateMachine     stateMachine;
        protected BattleUIView     battleUi;
        protected ControlStateType controlType;
        protected List<IFocusable> focusLayer;

        public ControlState(PlayerController controller, BattleSystem battleSystem, StateMachine stateMachine){
            this.controller   = controller;
            this.stateMachine = stateMachine;

            player   = battleSystem.player1;
            battleUi = battleSystem.BattleUI;
        }

        public virtual void Enter(){
            LoggerSystem.EnterForControlState(controlType);
            ChangingFocus(0);
        }

        public virtual void FrameUpdate(){
            HandleShowLog();
            FocusMoving();
            Decision();
        }

        private void HandleShowLog(){
            if (Keyboard.current.lKey.wasPressedThisFrame){
                LoggerSystem.ShowLogForUnity();
            }
        }

        public virtual void PhysicsUpdate(){}
        public virtual void Exit(){
            LoggerSystem.ExitForControlState(controlType);
            FocusObject = null;
        }

        protected virtual void FocusMoving(){
            if (controller.PlayerInput.RetrieveInputLeft()){
                ChangingFocus(-1);
            }
            if (controller.PlayerInput.RetrieveInputRight()){
                ChangingFocus(1);
            }
            if (controller.PlayerInput.RetrieveInputUp()){
                ChangingFocus(-2);
            }
            if (controller.PlayerInput.RetrieveInputDown()){
                ChangingFocus(2);
            }
        }
        
        protected void Decision(){
            if (controller.PlayerInput.RetrieveInputConfirm()){ DoConfirm(); }
            else if (controller.PlayerInput.RetrieveInputBack()){ DoBackspace(); }
        }
        public virtual void DecisionForMobile(){
            DoConfirm();
        }

        public bool IsClickByMouseOrTouch(){
            return controller.PlayerInput.RetrieveInputMouseLeft() ||
                   controller.PlayerInput.RetrieveInputTouch();
        }

        protected virtual void DoConfirm(){
            var focusObjectName = FocusObject.ToString().Remove(FocusObject.ToString().IndexOf(' '));
            Debug.Log($"Focus Obj: {focusObjectName}");
        }

        protected virtual void DoBackspace(){
            controller.EnterBasic();
        }

        protected void ChangingFocus(int indexDelta){
            bool isFromOtherLayer  = indexDelta == 0;
            int  newIndex          = player.CurrentFocus + indexDelta;
            bool hasChange         = isFromOtherLayer || (newIndex < focusLayer.Count && newIndex >= 0);
            if(!hasChange){ return ; }

            FocusObject?.OnFocusExit();
            player.CurrentFocus = isFromOtherLayer? 0 : newIndex;
            FocusObject          = focusLayer[player.CurrentFocus];
            FocusObject.OnFocusEnter();
        }

        public void ChangingFocus(IFocusable focusable){
            if(focusable == null){ Debug.Log($"Click error! focusable null."); }
            FocusObject?.OnFocusExit();
            FocusObject = focusable;
            FocusObject?.OnFocusEnter();
        }

        public void SetFocusLayer(List<IFocusable> focusLayer) => this.focusLayer = focusLayer;
        public bool IsOtherLayer(NonMonoButtonAction nonMonoButtonAction) => !focusLayer.Contains(nonMonoButtonAction);
        public bool IsOtherLayer(ControlStateType controlType) => this.controlType != controlType;
    }
}
