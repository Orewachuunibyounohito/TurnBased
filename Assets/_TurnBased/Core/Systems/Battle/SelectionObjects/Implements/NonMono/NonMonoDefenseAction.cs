using TurnBasedPractice.Character;
using TurnBasedPractice.Character.Controller;
using TurnBasedPractice.SkillSystem;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedPractice.BattleCore.Selection
{
    public class NonMonoDefenseAction : NonMonoButtonAction
    {
        public NonMonoDefenseAction(Button button, Hero user, params Hero[] targets) : base(button, user, targets){}

        public override void DoAction(MonoBehaviour mono){
            new Defense().Do(User, User.Targets);
        }

        public override void Interact(PlayerController playerController){
        }
    }
}
