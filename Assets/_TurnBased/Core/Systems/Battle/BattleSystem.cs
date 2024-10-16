using UnityEngine;

using TurnBasedPractice.Character;
using TurnBasedPractice.BattleCore.Selection;
using TurnBasedPractice.SO;
using TurnBasedPractice.AudioSystem;
using TurnBasedPractice.InfoSystem;

using UnityEngine.UI;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Localization;

namespace TurnBasedPractice.BattleCore
{
    public class BattleSystem : MonoBehaviour
    {
        [Header("Character")]
        [Indent]
        public Hero player1;
        [Indent]
        public Hero player2;

        // state: Start, Select, Perform, End
        public StateMachine StateMachine{ get; private set; }

        [Header("Buttons")]
        [Indent]
        public Button[] basicActionButtons;
        [Indent]
        public Button[] skillActionButtons;
        [Indent]
        public List<Button> inventoryActionButtons;

        public List<IFocusable> basicFocusables, skillFocusables, itemFocusables;

        [ReadOnly]
        [ShowInInspector]
        public BattleUIView BattleUI{ get; private set; }

        public Dictionary<BattleStateType , IState> BattleStates{ get; private set; }

        [Header("Used in Helper")]
        [Indent]
        public BattleInfoPlaySpeed playSpeed;

        private const BattleStateType START_STATE   = BattleStateType.Start;

        #region Unity Events
        private void Awake(){
            var initializeAsync = LocalizationSettings.SelectedLocaleAsync;
            if(initializeAsync.IsDone){
                SelectedLocaleAsyncCompleted(initializeAsync);
            }
            else{
                initializeAsync.Completed += SelectedLocaleAsyncCompleted;
            }
        }

        private void SelectedLocaleAsyncCompleted(AsyncOperationHandle<Locale> async){
            BattleInitializer.BattleInitialize(this);
            EnterBattle();
        }

        private void Start(){
            StateMachine.Initialize(BattleStates[START_STATE]);
        }

        private void Update(){
            StateMachine.CurrentState.FrameUpdate();
        }
        private void FixedUpdate(){
            StateMachine.CurrentState.PhysicsUpdate();
        }
        #endregion
        
        public void EnterBattle(){
            PlayBattleBgm();
        }
        
        private void PlayBattleBgm() => AudioPlayer.PlayBgm(BgmName.Battle);

        public void CreateStateMachine() => StateMachine = new StateMachine();
        public void SetBattleState(Dictionary<BattleStateType, IState> battleStates) =>
            BattleStates = battleStates;
        public void SetBattleUi(BattleUIView battleUI) => BattleUI = battleUI;

    }
}
