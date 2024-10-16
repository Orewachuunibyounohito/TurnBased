using System.Collections.Generic;
using System.Data;
using TurnBasedPractice.Extension;
using TurnBasedPractice.Items;

namespace TurnBasedPractice.LibrarySystem
{
    public class LibraryService : ILibraryService
    {
        Dictionary<LibraryType, ILibrary> _libraries = new Dictionary<LibraryType, ILibrary>();

        public LibraryService(){
            _libraries.Add(LibraryType.Item, new ItemLibrary());
            _libraries.Add(LibraryType.Skill, new SkillLibrary());
        }

        public object CreateGenericLibrary<T>(){
            return new ItemLibrary();
        }

        public T Get<T>(string name){
            var obj    = (T)_libraries[LibraryType.Item].GetObjectByName(name);
            var objOpt = new Optional<T>(obj);
            return objOpt.Put();
        }

        public Item GetItem(string name)
        {   
            var item    = (Item)_libraries[LibraryType.Item].GetObjectByName(name);
            var itemOpt = new Optional<Item>(item);
            return itemOpt.Put();
        }

        public Skill GetSkill(string name)
        {
            var skill    = (Skill)_libraries[LibraryType.Skill].GetObjectByName(name);
            var skillOpt = new Optional<Skill>(skill);
            return skillOpt.Put();
        }
    }
}