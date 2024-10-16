using NUnit.Framework;
using TurnBasedPractice.Localization;
using UnityEngine;
using UnityEngine.Localization;

public class LocaliztionTest : MonoBehaviour
{
    [Test]
    public void GetTranslatedPlayerConfigLabel(){
        LocalizedString translated = new LocalizedString("TurnBased", $"Template/{CommonStringTemplate.PlayerConfigLabel}");
        LocalizedString translatedFromSettings = LocalizationSettings.GetCommonString(CommonStringTemplate.PlayerConfigLabel);
        Debug.Log($"{translated.GetLocalizedString()}, {translatedFromSettings.GetLocalizedString()}");
    }
}
