using System;
using System.Collections.Generic;

public partial class MainMenuFlow
{
    [Serializable]
    class ScriptableVersionFields
    {
        public List<CommandAndObject> stepObjects;
        public bool Loop = false;
        public Queue<AnimateCommandFields> steps;
    }
}

