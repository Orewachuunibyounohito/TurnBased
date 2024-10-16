using System.Collections.Generic;

namespace TurnBasedPractice.LibrarySystem
{
    public interface ILibrary
    {
        LibraryType Type { get; }
        Dictionary<string , object> GetLibrary{ get; }

        bool   AddObject(string name, object obj);
        object GetObjectByName(string name);
    }
}