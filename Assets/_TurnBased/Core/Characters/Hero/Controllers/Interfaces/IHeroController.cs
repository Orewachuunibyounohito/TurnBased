using TurnBasedPractice.BattleCore.Selection;

namespace TurnBasedPractice.Character.Controller
{
    public interface IHeroController
    {
        public void          Selecting();
        public void          ActionFinished();
        public ICustomAction GetSelected();
        public void          PlayerDie(Hero attacker);
        public void          PlayerEscaped();
    }
}