using TurnBasedPractice.Character;

namespace TurnBasedPractice.SkillSystem
{
    public class Defense : Skill
    {
        public override SkillName skillName => SkillName.Defense;
        public Defense() : base(){
            // Name = "Defense";
            Description = "Defense";
            ExecuteTime = 0.01f;
        }

        public override void Do(Hero hero, params Hero[] targets){
            hero.Stats.Add(StatsSystem.StatsName.Guard, 0);
            hero.Controller.GetSelected().Finish();
        }
    }
}