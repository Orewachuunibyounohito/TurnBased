using NUnit.Framework;
using TurnBasedPractice.Localization;
using TurnBasedPractice.MainMenu.Views;
using UnityEngine.Localization;
using UnityLocalizationSettings = UnityEngine.Localization.Settings.LocalizationSettings;

public class MenuHelperTest
{
    private const string ZH_TW_LOCALE_NAME = "Chinese (Traditional) (zh-hant)";
    private const string EN_LOCALE_NAME = "English (en)";

    // 億, 兆, 京, 垓, 秭, 穰, 溝, 澗, 正, 載
    [Category("Menu/Helper")]
    [TestCase(1234UL, "一千二百三十四")]
    [TestCase(1004UL, "一千零四")]
    [TestCase(1014UL, "一千零一十四")]
    [TestCase(1204UL, "一千二百零四")]
    [TestCase(1034UL, "一千零三十四")]
    [TestCase(12345UL, "一萬二千三百四十五")]
    [TestCase(123456UL, "十二萬三千四百五十六")]
    [TestCase(1204567UL, "一百二十萬四千五百六十七")]
    [TestCase(10_0002_3456UL, "十億零二萬三千四百五十六")]
    [TestCase(102_0304_0506_0708_0900UL, "一百零二京零三百零四兆零五百零六億零七百零八萬零九百")]
    public void GivenNumberToChinese(ulong number, string expected){
        
        string actual = Helper.NumberToChinese(number);

        Assert.AreEqual(expected, actual);
    }

    [Category("Menu/Helper")]
    [TestCase(1UL, "配置一")]
    [TestCase(15UL, "配置十五")]
    [TestCase(101UL, "配置一百零一")]
    [TestCase(3124UL, "配置三千一百二十四")]
    [TestCase(1204567UL, "配置一百二十萬四千五百六十七")]
    [TestCase(10_0002_3456UL, "配置十億零二萬三千四百五十六")]
    [TestCase(102_0304_0506_0708_0900UL, "配置一百零二京零三百零四兆零五百零六億零七百零八萬零九百")]
    public void CreatePlayerConfigLabelWithNumberWhenLocateZhTW(ulong number, string expected){
        UnityLocalizationSettings.SelectedLocale = UnityLocalizationSettings.AvailableLocales
                                                                            .Locales
                                                                            .Find((locale) => locale.LocaleName == ZH_TW_LOCALE_NAME);
        LocalizedString commonString = LocalizationSettings.GetCommonString(CommonStringTemplate.PlayerConfigLabel);
        string playerConfigLabelTemplate = commonString.GetLocalizedString();

        // string actual = string.Format(LocalizationSettings.GetCommonTemplate(CommonStringTemplate.PlayerConfigLabel), Helper.NumberToLocale(number));

        string actual = string.Format(playerConfigLabelTemplate, Helper.NumberToLocale(number));

        Assert.AreEqual(expected, actual);
    }

}