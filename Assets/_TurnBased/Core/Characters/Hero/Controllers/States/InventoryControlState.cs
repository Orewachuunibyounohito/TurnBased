using System;
using TurnBasedPractice.BattleCore;
using TurnBasedPractice.BattleCore.Selection;

namespace TurnBasedPractice.Character.Controller
{
    public class InventoryControlState : ControlState
    {
        public InventoryControlState(PlayerController controller, BattleSystem battleSystem, StateMachine stateMachine) : base(controller, battleSystem, stateMachine){
            controlType = ControlStateType.Inventory;
        }

        public override void Enter(){
            base.Enter();
            OpenInventory();
        }

        private void OpenInventory(){
            if (!battleUi.InventoryPanel.gameObject.activeInHierarchy){
                battleUi.InventoryPanelSwitch();
            }
        }

        public override void Exit(){
            base.Exit();
            battleUi.InventoryPanelSwitch();
        }

        protected override void DoConfirm()
        {
            FocusObject.Interact(controller);
            controller.EnterTarget();
        }

        protected override void FocusMoving(){
            if (controller.PlayerInput.RetrieveInputUp()){
                ChangingFocus(-1);
                OnChangingFocus();
            }
            if (controller.PlayerInput.RetrieveInputDown()){
                ChangingFocus(1);
                OnChangingFocus();
            }
        }

        private void OnChangingFocus(){
            var newPosition = battleUi.InventoryContent.anchoredPosition;
            var upBoundary = battleUi.InventoryContent.anchoredPosition.y;
            var bottomBoundary = upBoundary + battleUi.PageHeight - battleUi.ItemHeight;
            var newPositionY = battleUi.ItemHeight * player.CurrentFocus;

            if(newPositionY < upBoundary){
                newPosition.y = newPositionY;
            }else if(newPositionY > bottomBoundary){
                newPosition.y = newPositionY + battleUi.ItemHeight - battleUi.PageHeight;
            }else{ return ; }
            battleUi.InventoryContent.anchoredPosition = newPosition;
        }

        public void RemoveItem(NonMonoInventoryItemAction inventoryItemAction){
            focusLayer.Remove(inventoryItemAction);
        }
    }
}
