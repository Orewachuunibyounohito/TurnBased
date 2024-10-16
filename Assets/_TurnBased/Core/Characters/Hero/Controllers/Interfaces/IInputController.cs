using UnityEngine;

namespace TurnBasedPractice.Character.Controller
{
    public interface IInputController
    {
        public bool RetrieveInputUp();
        public bool RetrieveInputDown();
        public bool RetrieveInputLeft();
        public bool RetrieveInputRight();
        public bool RetrieveInputConfirm();
        public bool RetrieveInputBack();
        public bool RetrieveInputMouseLeft();
        public bool RetrieveInputTouch();
        public Vector2 RetrieveMousePosition();
        public Vector2 RetrieveTouchPosition();
    }
}
