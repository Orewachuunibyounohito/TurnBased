using System;
using System.Collections.Generic;
using System.Linq;
using TurnBasedPractice.Localization;
using UnityEngine.Localization;

namespace TurnBasedPractice.MainMenu.Views
{
    public static class Helper
    {
        private static List<string> nestedDigitNames = new(){
            "", "十", "百", "千"
        };
        private static List<string> basedDigitNames = new(){
            "", "萬", "億", "兆", "京", "垓", "秭", "穰", "溝", "澗", "正", "載"
        };
        private static Dictionary<LocalizedString, string> translatedStrings = new();

        public static void Initialize(){
            foreach (var digitName in Enum.GetValues(typeof(DigitName)).Cast<DigitName>()){
                var localizedString = LocalizationSettings.GetDigitString(digitName);
                if(translatedStrings.TryAdd(localizedString, "")){
                    localizedString.StringChanged += UpdateTranslatedString(localizedString);
                }
            }
            foreach (var basedDigitName in Enum.GetValues(typeof(BasedDigitName)).Cast<BasedDigitName>()){
                var localizedString = LocalizationSettings.GetBasedDigitString(basedDigitName);
                if(translatedStrings.TryAdd(localizedString, "")){
                    localizedString.StringChanged += UpdateTranslatedString(localizedString);
                }
            }
            foreach (var nestedDigitName in Enum.GetValues(typeof(NestedDigitName)).Cast<NestedDigitName>()){
                var localizedString = LocalizationSettings.GetNestedDigitString(nestedDigitName);
                if(translatedStrings.TryAdd(localizedString, "")){
                    localizedString.StringChanged += UpdateTranslatedString(localizedString);
                }
            }
        }

        private static LocalizedString.ChangeHandler UpdateTranslatedString(LocalizedString localizedString){
            return (translatedString) => translatedStrings[localizedString] = translatedString;
        }

        public static string NumberToChinese(ulong number){
            string chinese = "";

            IEnumerator<string> digitName = basedDigitNames.GetEnumerator();
            ulong currentNestedNumber;
            string currentString;
            while(number != 0){
                digitName.MoveNext();
                currentNestedNumber = number % 10000;
                currentString = currentNestedNumber != 0? digitName.Current : "";
                currentString = NestedNumberToChinese(currentNestedNumber) + currentString;
                chinese = currentString + chinese;
                number /= 10000;
            }
            if (string.IsNullOrEmpty(chinese)){ return "零"; }
            chinese = chinese.IndexOf("零") == 0? chinese.Remove(0, 1) : chinese;
            chinese = chinese.IndexOf("十") == 1 && chinese.IndexOf("一") == 0? chinese.Remove(0, 1) : chinese;
            return chinese;
        }

        private static string NestedNumberToChinese(ulong nestedNumber){
            string chinese = "";
            if(nestedNumber == 0){ return ""; }
            IEnumerator<string> nestedDigitName = nestedDigitNames.GetEnumerator();
            ulong prevDigit = 0;
            ulong currentDigit;
            string currentString;
            bool hadNotZero = false;
            while(nestedNumber != 0){
                currentString = hadNotZero && prevDigit == 0 ? "零" : "";
                currentDigit = nestedNumber % 10;
                prevDigit = currentDigit;
                nestedNumber /= 10;
                nestedDigitName.MoveNext();
                if (currentDigit == 0) { continue; }
                hadNotZero = true;
                currentString = nestedDigitName.Current + currentString;
                currentString = DigitToChinese(currentDigit) + currentString;
                chinese = currentString + chinese;
            }
            chinese = nestedDigitName.Current != "千"? $"零{chinese}" : chinese;
            return chinese; 
        } 

        private static string DigitToChinese(ulong number){
            if(number >= 10){
                throw new Exception("Digit error!");
            }
            return number switch{
                0 => "零",
                1 => "一",
                2 => "二",
                3 => "三",
                4 => "四",
                5 => "五",
                6 => "六",
                7 => "七",
                8 => "八",
                9 => "九",
                _ => ""
            };
        }

        public static string NumberToLocale(ulong number){
            string localized = "";

            BasedDigitName basedDigitName = BasedDigitName.Zero;
            LocalizedString basedString;
            ulong currentNestedNumber;
            string currentString;
            while(number != 0){
                basedString = LocalizationSettings.GetBasedDigitString(basedDigitName);
                currentNestedNumber = number % 10000;
                currentString = currentNestedNumber != 0 && basedString != LocalizationSettings.NoneString? translatedStrings[basedString] : "";
                currentString = NestedNumberToLocale(currentNestedNumber) + currentString;
                localized = currentString + localized;
                number /= 10000;
                basedDigitName++;
            }
            if (string.IsNullOrEmpty(localized)){ return DigitToLocale(0); }
            localized = localized.IndexOf("零") == 0? localized.Remove(0, 1) : localized;
            localized = localized.IndexOf("十") == 1 && localized.IndexOf("一") == 0? localized.Remove(0, 1) : localized;    
            return localized;
        }

        public static string NestedNumberToLocale(ulong nestedNumber)
        {
            string localized = "";
            if (nestedNumber == 0) { return ""; }
            NestedDigitName nestedDigitName = NestedDigitName.Zero;
            LocalizedString nestedString;
            ulong prevDigit = 0;
            ulong currentDigit;
            string currentString;
            string currentNestedDigitName;
            bool hadNotZero = false;
            while (nestedNumber != 0){
                nestedString = LocalizationSettings.GetNestedDigitString(nestedDigitName);
                currentString =
                    hadNotZero && prevDigit == 0 ? DigitToLocale(0)
                                                 : "";
                currentDigit = nestedNumber % 10;
                prevDigit = currentDigit;
                nestedNumber /= 10;
                currentNestedDigitName = nestedString != LocalizationSettings.NoneString? translatedStrings[nestedString] : "";
                nestedDigitName++;
                if (currentDigit == 0) { continue; }
                hadNotZero = true;
                currentString = currentNestedDigitName + currentString;
                currentString = DigitToLocale(currentDigit) + currentString;
                localized = currentString + localized;
            }
            localized = ThousandNestedIsZero(localized) ?
                $"{DigitToLocale(0)}{localized}" : localized;
            return localized;
        }

        private static bool ThousandNestedIsZero(string localized){
            var localizedNestedThousand = LocalizationSettings.GetNestedDigitString(NestedDigitName.Thousand);
            return localized.IndexOf(translatedStrings[localizedNestedThousand]) != 1;
        }

        private static string DigitToLocale(ulong number){
            if(number >= 10){
                throw new Exception("Digit error!");
            }
            var localizedDigit = LocalizationSettings.GetDigitString((DigitName)number);
            return translatedStrings[localizedDigit] ?? "";
        }
    }
}
