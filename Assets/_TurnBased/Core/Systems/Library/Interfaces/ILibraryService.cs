using TurnBasedPractice.Items;

namespace TurnBasedPractice.LibrarySystem
{
    public interface ILibraryService
    {
        Skill GetSkill(string name);
        Item  GetItem(string name);
        T     Get<T>(string name);
        object CreateGenericLibrary<T>();
    }
}