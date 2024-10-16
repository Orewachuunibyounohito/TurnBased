using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using TurnBasedPractice.BattleCore.Selection;
using TurnBasedPractice.Character;
using TurnBasedPractice.Character.Controller;
using TurnBasedPractice.InfoSystem;
using TurnBasedPractice.Localization;
using TurnBasedPractice.Resource;
using TurnBasedPractice.SkillSystem;
using TurnBasedPractice.StatsSystem;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

using Object = UnityEngine.Object;

namespace TurnBasedPractice.BattleCore
{
    public class BattleInitializer
    {
        private BattleSystem battleSystem;

        private BattleInitializer(BattleSystem battleSystem){
            this.battleSystem = battleSystem;
        }

        public static void BattleInitialize(BattleSystem battleSystem){
            BattleInitializer initializer = new BattleInitializer(battleSystem);
            initializer.SetUpDefaultCase();
        }

        private void SetUpDefaultCase()
        {
            SetUpUsedState();
            var actionButtons   = SetUpBattleUi();
            var humanPlayer     = SetUpPlayers();
            var basicFocusables = SetUpNonMonoBasicButtons(actionButtons.basicActionButtons, humanPlayer);
            var itemFocusables  = CreateNonMonoInventoryItemButtonFromPlayer1InventoryList(humanPlayer);
            var skillFocusables = SetUpNonMonoSkillButton(actionButtons.skillActionButtons, humanPlayer);
            var targetFocusables = new List<IFocusable> { humanPlayer.Focusable, humanPlayer.Targets[0].Focusable };
            var layerContainer = new FocusLayerContainer{
                container = new Dictionary<ControlStateType, List<IFocusable>>{
                    {ControlStateType.Normal, basicFocusables},
                    {ControlStateType.Inventory, itemFocusables},
                    {ControlStateType.Attack, skillFocusables},
                    {ControlStateType.Target, targetFocusables}
                }
            };
            SetUpFocusLayer(layerContainer, humanPlayer);
            SetUpInfoText(humanPlayer);
        }
        
        private void SetUpUsedState(){
            battleSystem.CreateStateMachine();
            battleSystem.SetBattleState( 
                new Dictionary<BattleStateType, IState>(){
                    { BattleStateType.Start, new StartState(battleSystem, battleSystem.StateMachine) },
                    { BattleStateType.Select, new SelectState(battleSystem, battleSystem.StateMachine) },
                    { BattleStateType.Perform, new PerformState(battleSystem, battleSystem.StateMachine) },
                    { BattleStateType.End, new EndState(battleSystem, battleSystem.StateMachine) },
                    { BattleStateType.Other, new OtherState(battleSystem, battleSystem.StateMachine) }
                }
            );
        }

        private (Button[] basicActionButtons, Button[] skillActionButtons) SetUpBattleUi()
        {
            battleSystem.SetBattleUi(Object.FindAnyObjectByType<BattleUIView>());
            battleSystem.BattleUI.Init();
            battleSystem.basicActionButtons = battleSystem.BattleUI.ActionPanel.GetComponentsInChildren<Button>();
            battleSystem.skillActionButtons = battleSystem.BattleUI.SkillPanel.GetComponentsInChildren<Button>();
            return (battleSystem.basicActionButtons, battleSystem.skillActionButtons);
        }

        private Hero SetUpPlayers()
        {
            var playersSettings = Resources.Load<PlayersLoading>(SoPath.PLAYERS_CONFIG_PATH);

            battleSystem.player1 = GameObject.Find("Hero1").AddComponent<Hero>();
            battleSystem.player2 = GameObject.Find("Hero2").AddComponent<Hero>();
            
            battleSystem.player1 = SetUpPlayer(battleSystem.player1,
                                               battleSystem.player2,
                                               battleSystem.BattleUI.P1StatusPanel,
                                               playersSettings.P1Config());
            battleSystem.player1.SetDefaultInventory();

            battleSystem.player2 = SetUpPlayer(battleSystem.player2,
                                               battleSystem.player1,
                                               battleSystem.BattleUI.P2StatusPanel,
                                               playersSettings.P2Config());
            return battleSystem.player1;
        }
        private Hero SetUpPlayer(Hero player, Hero target, PlayerStatusView statusView, PlayerConfig config){
            player.Name = config.Name;
            player.Init(config.Stats);
            player.Controller = ControllerFactory.Generate(config.ControllerType, player, target, battleSystem);
            foreach(SkillName skillName in config.Skills){
                SkillLearner.HeroLearnSkill(player, skillName);
            }
            player.SetTargets(target);
            statusView.UpdateNameText(player.Name);
            statusView.UpdateLevelText(player.Stats[StatsName.Lv]);
            player.SetHpBar(statusView.HpBar);
            player.Die += (attacker) => ((PerformState)battleSystem.BattleStates[BattleStateType.Perform]).IsBattleOver = true;
            player.Die += (attacker) => {
                BattleResult result = new BattleResult{
                    winner = attacker,
                    loser  = player,
                    reason = VictoryReason.EnemyDefeat
                };
                if(result.winner == result.loser){
                    result.winner = player.Targets[0];
                }
                ((EndState)battleSystem.BattleStates[BattleStateType.End]).SetBattleResult(result);
            };
            return player;
        }

        private List<IFocusable> SetUpNonMonoBasicButtons(Button[] basicActionButtons, Hero humanPlayer){
            battleSystem.basicFocusables = new List<IFocusable>();

            var selectionType = SelectionType.Attack;
            foreach(var button in basicActionButtons){
                var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                IFocusable selection = SelectionFactory.GenerateSelection(battleSystem, selectionType, humanPlayer);
                battleSystem.basicFocusables.Add(selection);

                var localizedString = LocalizationSettings.GetSelectionString(selectionType);
                localizedString.StringChanged += UpdateTextForTextObject(buttonText);

                selectionType++;
            }
            var escapeAction = battleSystem.basicFocusables[3] as NonMonoEscapeAction;
            escapeAction.PlayerEscaped += (opponent) => HandlePlayerEscaped(humanPlayer, opponent);
            return battleSystem.basicFocusables;
        }
        
        private List<IFocusable> SetUpNonMonoSkillButton(Button[] skillActionButtons, Hero humanPlayer){
            var skills = humanPlayer.SkillInSlot;
            var skillCount = skills.Count;
            NonMonoSkillAction skillAction;
            battleSystem.skillFocusables = new List<IFocusable>();

            skillCount = skillCount > 4? 4 : skillCount;
            for(int index = 0; index < skillCount; index++){
                SelectionData skillSelectionData = SelectionData.SkillSelectionData(skillActionButtons[index],
                                                                                    skills[index],
                                                                                    humanPlayer,
                                                                                    humanPlayer.Targets);
                skillAction = SelectionFactory.GenerateSkillSelection(skillSelectionData) as NonMonoSkillAction;
                battleSystem.skillFocusables.Add(skillAction);
                
                var skillText = battleSystem.BattleUI.SkillTexts[index];
                var localizedString = LocalizationSettings.GetSkillString(skills[index].skillName);
                localizedString.StringChanged += UpdateTextForTextObject(skillText);
            }

            battleSystem.BattleUI.SkillPanelSwitch();
            return battleSystem.skillFocusables;
        }

        private void HandlePlayerEscaped(Hero escapedPlayer, Hero opponent){
            var battlePerformState = (PerformState)battleSystem.BattleStates[BattleStateType.Perform];
            var battleEndState = (EndState)battleSystem.BattleStates[BattleStateType.End];
            battlePerformState.IsBattleOver = true;
            battleEndState.SetBattleResult(
                new BattleResult
                {
                    winner = opponent,
                    loser = escapedPlayer,
                    reason = VictoryReason.EnemyEscaped
                }
            );
        }

        private List<IFocusable> CreateNonMonoInventoryItemButtonFromPlayer1InventoryList(Hero humanPlayer){
            battleSystem.BattleUI.InventoryContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            battleSystem.inventoryActionButtons = new List<Button>();
            battleSystem.itemFocusables         = new List<IFocusable>();
            float cellHeight    = battleSystem.BattleUI.InventoryContent.GetComponent<GridLayoutGroup>().cellSize.y;
            float currentHeight;
            foreach(var slot in humanPlayer.Inventory.Slots)
            {
                currentHeight = battleSystem.BattleUI.InventoryContent.rect.height + cellHeight;
                battleSystem.BattleUI.InventoryContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentHeight);
                var itemAction = Object.Instantiate(battleSystem.BattleUI.ItemActionPrefab, battleSystem.BattleUI.InventoryContent);
                var itemView = itemAction.AddComponent<InventoryItemView>().Init(slot);

                var inventoryItemAction =
                    SelectionFactory.GenerateInventoryItemSelection(
                        SelectionData.ItemSelectionData(itemAction.GetComponent<Button>(),
                                                        slot.item,
                                                        humanPlayer,
                                                        humanPlayer.Targets)) as NonMonoInventoryItemAction;
                battleSystem.itemFocusables.Add(inventoryItemAction);
                battleSystem.inventoryActionButtons.Add(inventoryItemAction.Button);

                var localizedString = LocalizationSettings.GetItemString(slot.item.Type);
                localizedString.StringChanged += UpdateTextForTextObject(itemView.DescriptionText);

                slot.ItemUsed += delegate { itemView.AmountText.SetText($"x{slot.amount}"); };
                slot.ItemUseUp += delegate
                {
                    InventoryControlState inventoryControlState = humanPlayer.Controller as PlayerController;
                    inventoryControlState.RemoveItem(inventoryItemAction);
                    Object.Destroy(itemAction);
                };
            }

            battleSystem.BattleUI.InventoryPanelSwitch();
            return battleSystem.itemFocusables;
        }

        private LocalizedString.ChangeHandler UpdateTextForTextObject(TextMeshProUGUI textObject){
            return (translatedString) => textObject.SetText(translatedString);
        }
        
        private void SetUpFocusLayer(FocusLayerContainer container, Hero humanPlayer)
        {
            foreach(var stateType in Enum.GetValues(typeof(ControlStateType)).Cast<ControlStateType>().Skip(1)){
                var controlState = (humanPlayer.Controller as PlayerController).ControlStates[stateType] as ControlState;
                controlState.SetFocusLayer(container[stateType]);
            }
        }

        private void SetUpInfoText(Hero humanPlayer){
            battleSystem.BattleUI.InfoPanelSwitch();
            BattleInfoSystem.Init(battleSystem.playSpeed, battleSystem.BattleUI.InfoPanel, ((PlayerController)humanPlayer.Controller).PlayerInput);    
        }

        private class FocusLayerContainer
        {
            public Dictionary<ControlStateType, List<IFocusable>> container;
            
            public List<IFocusable> this[ControlStateType stateType] => container[stateType];
        }
    }
}