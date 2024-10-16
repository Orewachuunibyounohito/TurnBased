using System.Linq;
using TurnBasedPractice.BattleCore;
using TurnBasedPractice.BattleCore.Selection;
using UnityEngine;

namespace TurnBasedPractice.Character.Controller
{
    public class TargetControlState : ControlState
    {
        public TargetControlState(PlayerController controller, BattleSystem battleSystem, StateMachine stateMachine) : base(controller, battleSystem, stateMachine){
            controlType = ControlStateType.Target;
        }
        
        public override void Enter(){
            base.Enter();
        }

        protected override void FocusMoving(){
            if (controller.PlayerInput.RetrieveInputLeft()){
                ChangingFocus(-1);
            }
            if (controller.PlayerInput.RetrieveInputRight()){
                ChangingFocus(1);
            }
        }

        public override void FrameUpdate()
        {
            base.FrameUpdate();
            SelectTargetAndConfirm();
        }

        public override void Exit(){
            FocusObject.OnFocusExit();
            base.Exit();
        }

        private void SelectTargetAndConfirm(){
            Vector2 pressedPosition = GetPressedPosition();
            if(pressedPosition.Equals(Vector2.positiveInfinity)){ return ; }
            Vector2 positionInWorld = Camera.main.ScreenToWorldPoint(pressedPosition);
            Collider2D collider = Physics2D.OverlapCircle(positionInWorld, 0.01f, LayerMask.GetMask("ActionButton"));
            if (collider == null) { return ; }
            Transform heroObject = collider.transform.parent.parent;
            if(heroObject.GetComponent<Hero>().Focusable == FocusObject){
                DoConfirm();
            }
            ChangingFocus(heroObject.GetComponent<Hero>().Focusable);
        }

        private Vector2 GetPressedPosition(){
            if(controller.PlayerInput.RetrieveInputMouseLeft()){
                return controller.PlayerInput.RetrieveMousePosition();
            }else if(controller.PlayerInput.RetrieveInputTouch()){
                return controller.PlayerInput.RetrieveTouchPosition();
            }
            return Vector2.positiveInfinity;
        }

        protected override void DoConfirm(){
            var targetFocusable = (HeroFocusable)FocusObject;
            controller.selectedAction = targetFocusable.UsedAction;
            targetFocusable.UsedAction.Targets = new Hero[]{ targetFocusable.Hero };
            targetFocusable.OnFocusExit();
            
            player.UpdatePriority();
            player.ActionPriority += 3;
            player.PhaseOK();
            controller.EndSelect();
        }

        public void SetAction(NonMonoButtonAction action){
            foreach(var focusable in focusLayer.Cast<HeroFocusable>()){
                focusable.UsedAction = action;
            }
        }
    }
}
