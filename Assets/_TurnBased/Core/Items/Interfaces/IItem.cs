using TurnBasedPractice.Character;
using UnityEngine;

namespace TurnBasedPractice.Items
{
    public interface IItem
    {
        long   Id{ get; }
        string Name{ get; }
        Sprite Icon{ get; }
        uint   Cost{ get; }
        string Description{ get; }
        
        public void Use(Hero user, Hero target);
    }
}
