using NUnit.Framework;
using TMPro;
using TurnBasedPractice;
using TurnBasedPractice.BattleCore;
using TurnBasedPractice.MainMenu;
using TurnBasedPractice.Resource;
using TurnBasedPractice.StatsSystem;
using UnityEngine;

public class PlayerInfoTest
{
    private RectTransform infoPanel;
    private PlayersLoading playersLoading;
    private PlayerConfig testConfig1;
    private PlayerConfig testConfig2;

    [SetUp]
    public void SetUp(){
        var prefab = Resources.Load<PrefabsSettings>(SoPath.PREFABS_SETTINGS_PATH).PlayerInfoPanel;
        infoPanel = Object.Instantiate(prefab)
                          .GetComponent<RectTransform>();
        infoPanel.name = "InfoPanel";

        playersLoading = Resources.Load<PlayersLoading>(SoPath.PLAYERS_CONFIG_PATH);
    }

    [Category("MainMenu/PlayerInfo")]
    [Test]
    public void GeneratePlayerInfoItemViewThenLabelTextAndValueTextAreAddedInWorld(){
        var infoItemView = PlayerInfoItemView.Generate("A", "10");

        (TextMeshProUGUI, TextMeshProUGUI) actual = (infoItemView.LabelText, infoItemView.ValueText);
        var viewInWorld = GameObject.Find(infoItemView.name).transform;
        (TextMeshProUGUI, TextMeshProUGUI) expected = (viewInWorld.Find("LabelText").GetComponent<TextMeshProUGUI>(),
                                                       viewInWorld.Find("ValueText").GetComponent<TextMeshProUGUI>());

        Assert.AreEqual(expected, actual);

        Object.DestroyImmediate(viewInWorld.gameObject);
    }

    [Category("MainMenu/PlayerInfo")]
    [TestCase("Player1")]
    public void GenerateNameItemByPlayeInfoItemPresenterWithName(string name){
        var presenter = PlayerInfoItemPresenter.GenerateNameItem(name);

        string actual = presenter.ToString();
        string expected = $"PlayerInfoItem:[label=Name, value={name}]\nPlayerInfoItemView:[LabelText=\"Name\", ValueText=\"{name}\"]";

        Assert.AreEqual(expected, actual);

        #if UNITY_EDITOR
        Object.DestroyImmediate(presenter.gameObject);
        #endif
    }

    [Category("MainMenu/PlayerInfo")]
    [TestCase(StatsName.MaxHp, 100)]
    [TestCase(StatsName.MaxMp, 5)]
    [TestCase(StatsName.Lv, 99)]
    [TestCase(StatsName.Strength, 13)]
    [TestCase(StatsName.Agility, 264)]
    [TestCase(StatsName.Wisdom, 67)]
    [TestCase(StatsName.Defense, 8)]
    public void GenerateStatsItemByPlayeInfoItemPresenterWithStatsNameAndValue(StatsName statsName, int value){
        var presenter = PlayerInfoItemPresenter.GenerateStatsItem(statsName, value);

        string actual = presenter.ToString();
        string expected = $"PlayerInfoItem:[label={statsName}, value={value}]\nPlayerInfoItemView:[LabelText=\"{statsName}\", ValueText=\"{value}\"]";

        Assert.AreEqual(expected, actual);

        #if UNITY_EDITOR
        Object.DestroyImmediate(presenter.gameObject);
        #endif
    }

    [Category("MainMenu/PlayerInfo")]
    [Test]
    public void NewPlayerInfoAndSetParentThenParentIsInfoPanel(){
        var info = new PlayerInfo();
        info.SetParentAndAdjustScale(infoPanel);

        Transform actual = info.GetContainer().parent;
        Transform expected = infoPanel;

        Assert.AreEqual(expected, actual);
    }
    
    [Category("MainMenu/PlayerInfo")]
    [Test]
    public void PlayerInfoAddItemThenItemsContainsItem(){
        var info = new PlayerInfo();
        info.SetParentAndAdjustScale(infoPanel);
        var infoItem = PlayerInfoItemPresenter.GenerateNameItem("ZZZ");

        info.AddItem(infoItem);

        Assert.IsTrue(info.GetItems().Contains(infoItem));
    }

    [Category("MainMenu/PlayerInfo")]
    [Test]
    public void GeneratePlayerInfoByPlayerConfigThenItemsIsCompleted(){
        var playerConfig = playersLoading.P1Config();
        var info = new PlayerInfo(playerConfig);
        info.SetParentAndAdjustScale(infoPanel);
        var items = info.GetItems();
        string errorMessage = "Unrecognized error!";

        bool actual = false;
        bool isExist = items.Exists((item) => {
                var isExist = item.ItemContent.label == "Name" && item.ItemContent.value == playerConfig.Name;
                if(!isExist){
                    errorMessage = NameItemErrorMessage(item, playerConfig.Name);
                    return false;
                }
                return true;
            }
        );
        actual = isExist;
        if(!isExist){
            Assert.IsTrue(actual, errorMessage);
            return ;
        }

        int statsItemCount = items.Count - 1;
        isExist = statsItemCount == playerConfig.Stats.RequiredStats.Count;
        actual = isExist;
        if(!isExist){
            errorMessage = StatsItemCountErrorMessage(items.Count-1, playerConfig);
            Assert.IsTrue(actual, errorMessage);
            return ;
        }

        foreach(var statsData in playerConfig.Stats.RequiredStats){
            isExist = items.Exists((item) => {
                var isExist = item.ItemContent.label == $"{statsData.Name}" && item.ItemContent.value == $"{statsData.Value}";
                if(!isExist){
                    errorMessage = StatsItemErrorMessage(item, statsData);
                    return false;
                }
                return true;
            });
            if(!isExist){ break ; }
        }
        actual = isExist;

        Assert.IsTrue(actual, errorMessage);

        string NameItemErrorMessage(PlayerInfoItemPresenter item, string name) => 
            $"Label: {item.ItemContent.label} & Name\nValue: {item.ItemContent.value} & {name}";
        string StatsItemErrorMessage(PlayerInfoItemPresenter item, Stats.Data statsData) => 
            $"Label: {item.ItemContent.label} & {statsData.Name}\nValue: {item.ItemContent.value} & {statsData.Value}";
        string StatsItemCountErrorMessage(int statsItemCount, PlayerConfig config) => 
            $"StatsItem.Count: {items.Count-1} & {playerConfig.Stats.RequiredStats.Count}";
    }

}