using TurnBasedPractice.BattleCore;

namespace TurnBasedPractice.MainMenu.Presenters
{
    public class InfoSet
    {
        private PlayerInfo p1Info;
        private PlayerInfo p2Info;
        
        public InfoSet(PlayerInfo p1Info, PlayerInfo p2Info){
            this.p1Info = p1Info;
            this.p2Info = p2Info;
        }

        public void Hide(){
            p1Info.Visible = false;
            p2Info.Visible = false;
        }

        public void Show(){
            p1Info.Visible = true;
            p2Info.Visible = true;
        }
    }
}
