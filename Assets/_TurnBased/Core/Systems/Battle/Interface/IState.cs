using TurnBasedPractice.Character.Controller;

namespace TurnBasedPractice.BattleCore
{
    public interface IState
    {
        public void Enter();
        public void FrameUpdate();
        public void PhysicsUpdate();
        public void Exit();
    }
}
