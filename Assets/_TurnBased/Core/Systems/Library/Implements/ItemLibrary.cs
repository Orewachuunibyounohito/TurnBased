using System.Collections.Generic;
using TurnBasedPractice.Items;

namespace TurnBasedPractice.LibrarySystem
{
    public class ItemLibrary : ILibrary
    {
        private readonly LibraryType                _type;
        private readonly Dictionary<string, Item> _library;

        public LibraryType                Type       => _type;
        public Dictionary<string, Item> GetLibrary => _library;

        Dictionary<string, object> ILibrary.GetLibrary => throw new System.NotImplementedException();

        public ItemLibrary(){
            _type    = LibraryType.Item;
            _library = new Dictionary<string, Item>();
        }

        public bool AddObject(string name, Item obj){
            return _library.TryAdd(name, obj);
        }

        public Item GetObjectByName(string name){
            return _library[name];
        }

        bool ILibrary.AddObject(string name, object obj){
            return _library.TryAdd(name, (Item)obj);
        }

        object ILibrary.GetObjectByName(string name){
            return _library[name];
        }
    }
}