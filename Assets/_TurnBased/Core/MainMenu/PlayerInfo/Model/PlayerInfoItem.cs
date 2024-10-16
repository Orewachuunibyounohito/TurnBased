using TurnBasedPractice.StatsSystem;

namespace TurnBasedPractice.MainMenu
{
    public class PlayerInfoItem
    {
        private string label;
        private string value;

        private PlayerInfoItem(string label, string value){
            this.label = label;
            this.value = value;
        }

        public override string ToString() => $"PlayerInfoItem:[label={label}, value={value}]";
        #if UNITY_EDITOR
        public (string label, string value) GetContent() => (label, value);
        #endif

        public static PlayerInfoItem GenerateNameItem(string name) =>
            new PlayerInfoItem("Name", name);

        public static PlayerInfoItem GenerateStatsItem(StatsName name, int value) =>
            new PlayerInfoItem($"{name}", $"{value}");

    }
}