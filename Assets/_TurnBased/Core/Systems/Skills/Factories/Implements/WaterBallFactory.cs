using TurnBasedPractice.AudioSystem;
using TurnBasedPractice.SO;

namespace TurnBasedPractice.SkillSystem
{
    public class WaterBallFactory : ISkillFactory
    {
        public Skill GenerateSkill(){
            var waterBall = new WaterBall();
            waterBall.skillUsed += delegate{ AudioPlayer.PlaySfx(SfxName.WaterBall); };
            return waterBall;
        }
    }
}