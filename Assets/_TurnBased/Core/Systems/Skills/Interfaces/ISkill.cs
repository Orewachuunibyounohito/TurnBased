using TurnBasedPractice.Character;

namespace TurnBasedPractice.SkillSystem
{
    public interface ISkill
    {
        public string Name{ get; set; }
        public string Description{ get; set; }
        public float  ExecuteTime{ get; set; }

        public void Do(Hero user, params Hero[] targets);
    }
}