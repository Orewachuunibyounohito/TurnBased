using TurnBasedPractice.AudioSystem;
using TurnBasedPractice.SO;

namespace TurnBasedPractice.SkillSystem
{
    public class ImpactFactory : ISkillFactory
    {
        public Skill GenerateSkill(){
            var impact = new Impact();
            impact.skillUsed += delegate{ AudioPlayer.PlaySfx(SfxName.Impact); };
            return impact;
        }
    }
}