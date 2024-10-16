using TurnBasedPractice.BattleCore;

namespace TurnBasedPractice.Character.Controller
{
    public class ControllerFactory
    {
        public static IHeroController Generate(PlayerControllerType controllerType, Hero user, Hero target, BattleSystem battleSystem){
            return controllerType switch{
                PlayerControllerType.Human    => new PlayerController(user, battleSystem),
                PlayerControllerType.NonHuman => new NonPlayerController(user, target),
                _ => throw new System.Exception("Unrecognized ControllerType.")
            };
        }
    }
}