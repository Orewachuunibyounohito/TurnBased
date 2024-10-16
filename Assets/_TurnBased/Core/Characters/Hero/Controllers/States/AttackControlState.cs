using System;
using TurnBasedPractice.BattleCore;
using TurnBasedPractice.BattleCore.Selection;

namespace TurnBasedPractice.Character.Controller
{

    public class AttackControlState : ControlState
    {
        public AttackControlState(PlayerController controller, BattleSystem battleSystem, StateMachine stateMachine) : base(controller, battleSystem, stateMachine){
            controlType = ControlStateType.Attack;
        }

        public override void Enter(){
            base.Enter();
            OpenSkill();
        }

        private void OpenSkill(){
            if (!battleUi.SkillPanel.gameObject.activeInHierarchy){
                battleUi.SkillPanelSwitch();
            }
        }

        public override void Exit(){
            base.Exit();
            battleUi.SkillPanelSwitch();
        }

        protected override void DoConfirm()
        {
            var customAction = (NonMonoSkillAction)FocusObject;
            if (NotEnoughToUse(customAction)){
                new NotEnoughHint(customAction.skill);
                return;
            }

            controller.selectedAction = customAction;
            customAction.Targets = player.Targets;
            player.UpdatePriority();
            player.PhaseOK();
            controller.EndSelect();
        }

        private static bool NotEnoughToUse(NonMonoSkillAction customAction) => !customAction.CanUse;
    }
}
