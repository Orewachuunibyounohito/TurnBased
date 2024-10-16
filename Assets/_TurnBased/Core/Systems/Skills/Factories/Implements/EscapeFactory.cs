using TurnBasedPractice.AudioSystem;
using TurnBasedPractice.SO;

namespace TurnBasedPractice.SkillSystem
{
    public class EscapeFactory : ISkillFactory
    {
        public Skill GenerateSkill(){
            var escape = new Escape();
            // StaticElectricity.skillUsed += delegate{ AudioPlayer.PlaySfx(SfxName.WaterBall); };
            return escape;
        }
    }
}