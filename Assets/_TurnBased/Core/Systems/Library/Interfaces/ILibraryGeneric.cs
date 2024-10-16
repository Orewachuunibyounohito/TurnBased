using System.Collections.Generic;

namespace TurnBasedPractice.LibrarySystem
{
    public interface ILibrary<T>
    {
        LibraryType Type { get; }
        Dictionary<string , T> GetLibrary{ get; }

        bool   AddObject(string name, T obj);
        T GetObjectByName(string name);
    }
}