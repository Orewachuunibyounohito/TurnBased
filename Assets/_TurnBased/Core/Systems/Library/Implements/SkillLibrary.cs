using System.Collections.Generic;

namespace TurnBasedPractice.LibrarySystem
{
    public class SkillLibrary : ILibrary
    {
        private readonly LibraryType                _type;
        private readonly Dictionary<string, Skill> _library;

        public LibraryType                Type       => _type;
        public Dictionary<string, Skill> GetLibrary => _library;

        Dictionary<string, object> ILibrary.GetLibrary => throw new System.NotImplementedException();

        public SkillLibrary(){
            _type    = LibraryType.Item;
            _library = new Dictionary<string, Skill>();
        }

        public bool AddObject(string name, Skill obj){
            return _library.TryAdd(name, obj);
        }

        public Skill GetObjectByName(string name){
            return _library[name];
        }

        bool ILibrary.AddObject(string name, object obj){
            return AddObject(name, (Skill)obj);
        }

        object ILibrary.GetObjectByName(string name){
            return GetObjectByName(name);
        }
    }
}