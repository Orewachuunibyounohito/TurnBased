using System;
using TurnBasedPractice.Character;
using TurnBasedPractice.Character.Controller;
using TurnBasedPractice.Localization;
using TurnBasedPractice.SkillSystem;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedPractice.BattleCore.Selection
{
    public class NonMonoEscapeAction : NonMonoButtonAction
    {
        private Escape escape;

        public event Action<Hero> PlayerEscaped;

        public NonMonoEscapeAction(Button button, Hero user, params Hero[] targets) : base(button, user, targets){
            escape = SkillFactory.GenerateSkill(SkillName.Escape) as Escape;
        }

        public override void DoAction(MonoBehaviour mono){
            escape.Do(User, User.Targets);
            if(!escape.IsEscaped){
                ExecuteInfo = EscapeFailedText();
                base.DoAction(mono);
                return ;
            }

            User.Controller.ActionFinished();
            foreach(var target in User.Targets){
                PlayerEscaped.Invoke(target);
                target.Controller.ActionFinished();
            }
        }

        public override void Interact(PlayerController playerController){
        }

        private string EscapeFailedText() => 
            string.Format(LocalizationSettings.GetCommonString(CommonStringTemplate.EscapeFailed).GetLocalizedString(),
                          User.Name);
    }
}
