using TurnBasedPractice.Resource;
using TurnBasedPractice.BattleCore;
using TurnBasedPractice.BattleCore.Selection;

using System.Collections.Generic;
using UnityEngine;
using System;

namespace TurnBasedPractice.Character.Controller
{
    public class PlayerController : IHeroController
    {
        public static implicit operator NormalControlState(PlayerController playerController) => playerController.ControlStates[ControlStateType.Normal] as NormalControlState;
        public static implicit operator AttackControlState(PlayerController playerController) => playerController.ControlStates[ControlStateType.Attack] as AttackControlState;
        public static implicit operator InventoryControlState(PlayerController playerController) => playerController.ControlStates[ControlStateType.Inventory] as InventoryControlState;
        public static implicit operator TargetControlState(PlayerController playerController) => playerController.ControlStates[ControlStateType.Target] as TargetControlState;

        private Hero _player;
        private BattleSystem _battleSystem;

        public ICustomAction    selectedAction;
        public IInputController PlayerInput{ get; private set; }
        public StateMachine     stateMachine;

        public Dictionary<ControlStateType, IState> ControlStates{ get; private set; }

        public PlayerController(Hero hero, BattleSystem battleSystem){
            _player = hero;
            _battleSystem = battleSystem;
            
            PlayerInput = new PlayerInput_NonMono(Resources.Load<PlayerInputSO>(SoPath.DEFAULT_INPUT_PATH));

            stateMachine = new StateMachine();
            ControlStates = new Dictionary<ControlStateType, IState>{
                { ControlStateType.None, new NoneControlState(this) },
                { ControlStateType.Normal, new NormalControlState(this, battleSystem, stateMachine) },
                { ControlStateType.Attack, new AttackControlState(this, battleSystem, stateMachine) },
                { ControlStateType.Inventory, new InventoryControlState(this, battleSystem, stateMachine) },
                { ControlStateType.Target, new TargetControlState(this, battleSystem, stateMachine) }
            };
            stateMachine.Initialize(ControlStates[ControlStateType.None]);
        }

        public void Selecting(){
            stateMachine.CurrentState.FrameUpdate();
        }

        public void          ActionFinished()         => selectedAction.Finish();
        public ICustomAction GetSelected()            => selectedAction;
        public void          PlayerDie(Hero attacker) => selectedAction = new DeathAction(_player, attacker);
        public void          PlayerEscaped()          => selectedAction = new EscapedAction(_player);

        public void EnterBasic() => stateMachine.ChangeState(ControlStates[ControlStateType.Normal]);
        public void EnterAttack() => stateMachine.ChangeState(ControlStates[ControlStateType.Attack]);
        public void EnterInventory() => stateMachine.ChangeState(ControlStates[ControlStateType.Inventory]);
        public void EnterTarget() => stateMachine.ChangeState(ControlStates[ControlStateType.Target]);
        public void EndSelect() => stateMachine.ChangeState(ControlStates[ControlStateType.None]);

        public bool IsSelectingPhase() => _battleSystem.StateMachine.CurrentState == _battleSystem.BattleStates[BattleStateType.Select];
    }
}
