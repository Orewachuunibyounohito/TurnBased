using TurnBasedPractice.BattleCore;
using TurnBasedPractice.BattleCore.Selection;

namespace TurnBasedPractice.Character.Controller
{

    public class NormalControlState : ControlState
    {   
        public NormalControlState(PlayerController controller, BattleSystem battleSystem, StateMachine stateMachine) : base(controller, battleSystem, stateMachine){
            controlType = ControlStateType.Normal;
        }

        public override void Enter(){
            base.Enter();
        }

        protected override void DoConfirm(){
            if (FocusObject is ICustomAction){
                var custemAction = (ICustomAction)FocusObject;
                controller.selectedAction = custemAction;
                custemAction.Targets = player.Targets;
                player.UpdatePriority();
                player.ActionPriority += 3;
                player.PhaseOK();
                controller.EndSelect();
            }else{
                FocusObject.Interact(controller);
            }
        }
    }
}
