using System;
using TurnBasedPractice.Character;
using TurnBasedPractice.Character.Controller;

namespace TurnBasedPractice.BattleCore.Selection
{
    public static class SelectionFactory
    {
        public static IFocusable GenerateAttackSelection(SelectionData selectionData){
            return new NonMonoContainerObject(selectionData.Button, selectionData.SelfLayer, selectionData.ChildLayer, (PlayerController)selectionData.User.Controller);
        }
        public static IFocusable GenerateInventorySelection(SelectionData selectionData){
            return new NonMonoContainerObject(selectionData.Button, selectionData.SelfLayer, selectionData.ChildLayer, (PlayerController)selectionData.User.Controller);
        }
        public static ICustomAction GenerateDefenseSelection(SelectionData selectionData){
            return new NonMonoDefenseAction(selectionData.Button, selectionData.User, selectionData.Targets);;
        }
        public static ICustomAction GenerateEscapeSelection(SelectionData selectionData){
            return new NonMonoEscapeAction(selectionData.Button, selectionData.User, selectionData.Targets);
        }
        public static ICustomAction GenerateSkillSelection(SelectionData selectionData){
            return new NonMonoSkillAction(selectionData.Button, selectionData.Skill, selectionData.User, selectionData.Targets);
        }
        public static ICustomAction GenerateInventoryItemSelection(SelectionData selectionData){
            return new NonMonoInventoryItemAction(selectionData.Button, selectionData.Item, selectionData.User, selectionData.Targets);
        }

        public static IFocusable GenerateSelection(BattleSystem battleSystem, SelectionType selectionType, Hero humanPlayer){
            return selectionType switch{
                SelectionType.Attack =>
                    GenerateAttackSelection(SelectionData.AttackSelectionData(battleSystem.basicActionButtons[0], humanPlayer)),
                SelectionType.Inventory => 
                    GenerateInventorySelection(SelectionData.InventorySelectionData(battleSystem.basicActionButtons[1], humanPlayer)),
                SelectionType.Defense =>
                    GenerateDefenseSelection(SelectionData.DefenseSelectionData(battleSystem.basicActionButtons[2],
                                                                                humanPlayer,
                                                                                humanPlayer.Targets)) as IFocusable,
                SelectionType.Escape =>
                    GenerateEscapeSelection(SelectionData.EscapeSelectionData(battleSystem.basicActionButtons[3],
                                                                              humanPlayer,
                                                                              humanPlayer.Targets)) as IFocusable,
                _ => throw new Exception("Unrecognized SelectionType.")
            };
        }
    }
}