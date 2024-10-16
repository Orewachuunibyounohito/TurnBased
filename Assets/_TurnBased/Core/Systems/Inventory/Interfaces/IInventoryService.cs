using TurnBasedPractice.Character;
using TurnBasedPractice.Items;

namespace TurnBasedPractice.InventorySystem
{
    public interface IInventoryService
    {
        public void UseItem(IItem item, Hero user, Hero target);
    }
}
