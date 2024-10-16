using System.Collections.Generic;
using TurnBasedPractice.BattleCore.Selection;
using TurnBasedPractice.Effects;
using TurnBasedPractice.Items;
using TurnBasedPractice.SkillSystem;
using UnityEngine.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TurnBasedPractice.Localization
{
    public class LocalizationSettings
    {
        private const string USED_TABLE = "TurnBased";
        
        private const string SELECTION_ENTRY_KEY_TEMPLATE = "Selection/{0}";
        private const string COMMON_ENTRY_KEY_TEMPLATE = "Template/{0}";
        private const string ITEM_ENTRY_KEY_TEMPLATE = "Item/{0}";
        private const string SKILL_ENTRY_KEY_TEMPLATE = "Skill/{0}";
        private const string BUFF_ENTRY_KEY_TEMPLATE = "Buff/{0}";
        private const string BUFF_WITH_PHASE_ENTRY_KEY_TEMPLATE = "Buff/{0}/{1}";
        private const string DIGIT_ENTRY_KEY_TEMPLATE = "Digit/{0}";
        private const string BASED_DIGIT_ENTRY_KEY_TEMPLATE = "Digit/Based{0}";
        private const string NESTED_DIGIT_ENTRY_KEY_TEMPLATE = "Digit/Nested{0}";
        
        private static Dictionary<string, LocalizedString> localizedStringDictionary = new Dictionary<string, LocalizedString>();

        public static LocalizedString NoneString = new LocalizedString();

        static LocalizationSettings(){
            var async = UnityEngine.Localization.Settings.LocalizationSettings.SelectedLocaleAsync;
            if(async.IsDone){
                InitializeCompleted(async);
            }else{
                async.Completed += InitializeCompleted;
            }
        }

        private static void InitializeCompleted(AsyncOperationHandle<Locale> async)
        {
            AddSelectionEntryIntoDictionary();
            AddItemEntryIntoDictionary();
            AddSkillEntryIntoDictionary();
            AddEffectEntryIntoDictionary();
            AddCommonTemplateEntryIntoDictionary();
            AddDigitEntryIntoDictionary();
            AddBasedDigitEntryIntoDictionary();
            AddNestedDigitEntryIntoDictionary();
        }

        private static void AddSelectionEntryIntoDictionary()
        {
            for(SelectionType selectionType = SelectionType.Attack;
                selectionType < SelectionType.Target;
                selectionType++)
            {
                localizedStringDictionary.Add(GetSelectionEntryKey(selectionType),
                                              new LocalizedString(USED_TABLE, GetSelectionEntryKey(selectionType)));
            }
        }
        private static void AddItemEntryIntoDictionary()
        {
            for(ItemName itemName = ItemName.HealthPotion, lastItem = ItemName.Panacea;
                itemName <= lastItem;
                itemName++)
            {
                localizedStringDictionary.Add(GetItemEntryKey(itemName),
                                              new LocalizedString(USED_TABLE, GetItemEntryKey(itemName)));
                if (itemName == ItemName.Panacea) { break; }
            }
        }
        private static void AddSkillEntryIntoDictionary()
        {
            for(SkillName skillName = SkillName.Impact, lastItem = SkillName.Escape;
                skillName <= lastItem;
                skillName++)
            {
                localizedStringDictionary.Add(GetSkillEntryKey(skillName),
                                              new LocalizedString(USED_TABLE, GetSkillEntryKey(skillName)));
                if (skillName == SkillName.Escape) { break; }
            }
        }
        private static void AddEffectEntryIntoDictionary()
        {
            for(EffectName effectName = EffectName.HpRecovery;
                effectName <= EffectName.Asleep; effectName++)
            {
                localizedStringDictionary.Add(GetBuffEntryKey(effectName),
                                              new LocalizedString(USED_TABLE, GetBuffEntryKey(effectName)));
                for(BuffPhase buffPhase = BuffPhase.Bestowed;
                    buffPhase <= BuffPhase.Lift; buffPhase++)
                {
                    localizedStringDictionary.Add(GetBuffWithPhaseEntryKey(effectName, buffPhase),
                                                  new LocalizedString(USED_TABLE, GetBuffWithPhaseEntryKey(effectName, buffPhase)));
                    if(buffPhase == BuffPhase.Lift){ break; }
                }
                if (effectName == EffectName.Asleep) { break; }
            }
        }
        private static void AddCommonTemplateEntryIntoDictionary()
        {
            for(CommonStringTemplate template = CommonStringTemplate.SkillUse;
                template <= CommonStringTemplate.PlayerConfigLabel; template++)
            {   
                localizedStringDictionary.Add(GetCommonEntryKey(template),
                                              new LocalizedString(USED_TABLE, GetCommonEntryKey(template)));
            }
        }

        private static void AddDigitEntryIntoDictionary()
        {
            for(DigitName digitName = DigitName.Zero;
                digitName <= DigitName.Nine; digitName++)
            {   
                localizedStringDictionary.Add(GetDigitEntryKey(digitName),
                                              new LocalizedString(USED_TABLE, GetDigitEntryKey(digitName)));
                if(digitName == DigitName.Nine) { break; }
            }
        }
        private static void AddBasedDigitEntryIntoDictionary()
        {
            for(BasedDigitName basedDigitName = BasedDigitName.Zero;
                basedDigitName <= BasedDigitName.Eleven; basedDigitName++)
            {
                localizedStringDictionary.Add(GetBasedDigitEntryKey(basedDigitName),
                                              new LocalizedString(USED_TABLE, GetBasedDigitEntryKey(basedDigitName)));

                if (basedDigitName == BasedDigitName.Eleven) break;
            }
        }
        private static void AddNestedDigitEntryIntoDictionary()
        {
            for(NestedDigitName nestedDigitName = NestedDigitName.Zero;
                nestedDigitName <= NestedDigitName.Thousand; nestedDigitName++)
            {
                localizedStringDictionary.Add(GetNestedDigitEntryKey(nestedDigitName),
                                              new LocalizedString(USED_TABLE, GetNestedDigitEntryKey(nestedDigitName)));

                if (nestedDigitName == NestedDigitName.Thousand) break;
            }
        }

        private static string GetSelectionEntryKey(SelectionType selectionType) =>
            string.Format(SELECTION_ENTRY_KEY_TEMPLATE, selectionType);
        private static string GetCommonEntryKey(CommonStringTemplate commonType) =>
            string.Format(COMMON_ENTRY_KEY_TEMPLATE, commonType);
        private static string GetItemEntryKey(ItemName itemName) =>
            string.Format(ITEM_ENTRY_KEY_TEMPLATE, itemName);
        private static string GetSkillEntryKey(SkillName skillName) =>
            string.Format(SKILL_ENTRY_KEY_TEMPLATE, skillName);
        private static string GetBuffEntryKey(EffectName effectName) =>
            string.Format(BUFF_ENTRY_KEY_TEMPLATE, effectName);
        private static string GetBuffWithPhaseEntryKey(EffectName effectName, BuffPhase phase) =>
            string.Format(BUFF_WITH_PHASE_ENTRY_KEY_TEMPLATE, effectName, phase);
        private static string GetDigitEntryKey(DigitName digitName) =>
            string.Format(DIGIT_ENTRY_KEY_TEMPLATE, digitName);
        private static string GetBasedDigitEntryKey(BasedDigitName basedDigitName) =>
            string.Format(BASED_DIGIT_ENTRY_KEY_TEMPLATE, (int)basedDigitName);
        private static string GetNestedDigitEntryKey(NestedDigitName nestedDigitName) =>
            string.Format(NESTED_DIGIT_ENTRY_KEY_TEMPLATE, (int)nestedDigitName);
        
        public static LocalizedString GetSelectionString(SelectionType selectionType) =>
            localizedStringDictionary[GetSelectionEntryKey(selectionType)];
        public static LocalizedString GetCommonString(CommonStringTemplate commonType) =>
            localizedStringDictionary[GetCommonEntryKey(commonType)];
        public static LocalizedString GetItemString(ItemName itemName) =>
            localizedStringDictionary[GetItemEntryKey(itemName)];
        public static LocalizedString GetSkillString(SkillName skillName) =>
            localizedStringDictionary[GetSkillEntryKey(skillName)];
        public static LocalizedString GetBuffString(EffectName effectName) =>
            localizedStringDictionary[GetBuffEntryKey(effectName)];
        public static LocalizedString GetBuffString(EffectName effectName, BuffPhase phase) =>
            localizedStringDictionary[GetBuffWithPhaseEntryKey(effectName, phase)];
        public static LocalizedString GetDigitString(DigitName digitName) =>
            localizedStringDictionary[GetDigitEntryKey(digitName)];
        public static LocalizedString GetBasedDigitString(BasedDigitName basedDigitName) =>
            basedDigitName != BasedDigitName.Zero? localizedStringDictionary[GetBasedDigitEntryKey(basedDigitName)] : NoneString;
        public static LocalizedString GetNestedDigitString(NestedDigitName nestedDigitName) =>
            nestedDigitName != NestedDigitName.Zero? localizedStringDictionary[GetNestedDigitEntryKey(nestedDigitName)] : NoneString;        
    }
}