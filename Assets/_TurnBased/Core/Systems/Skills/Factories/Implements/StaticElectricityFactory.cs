using TurnBasedPractice.AudioSystem;
using TurnBasedPractice.SO;

namespace TurnBasedPractice.SkillSystem
{
    public class StaticElectricityFactory : ISkillFactory
    {
        public Skill GenerateSkill(){
            var staticElectricity = new StaticElectricity();
            // StaticElectricity.skillUsed += delegate{ AudioPlayer.PlaySfx(SfxName.WaterBall); };
            return staticElectricity;
        }
    }
}