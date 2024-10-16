using UnityEngine;
using UnityEngine.InputSystem;

namespace TurnBasedPractice.Character.Controller
{
    public class PlayerInput_NonMono : IInputController
    {
        [SerializeField] private KeyCode inputUp;
        [SerializeField] private KeyCode inputDown;
        [SerializeField] private KeyCode inputLeft;
        [SerializeField] private KeyCode inputRight;
        [SerializeField] private KeyCode inputBack;
        [SerializeField] private KeyCode inputConfirm;
        [SerializeField] private KeyCode altInputUp;
        [SerializeField] private KeyCode altInputDown;
        [SerializeField] private KeyCode altInputLeft;
        [SerializeField] private KeyCode altInputRight;
        [SerializeField] private KeyCode altInputBack;
        [SerializeField] private KeyCode altInputConfirm;

        public PlayerInput_NonMono(PlayerInputSO playerInput){
            inputUp         = playerInput.inputUp;
            inputDown       = playerInput.inputDown;
            inputLeft       = playerInput.inputLeft;
            inputRight      = playerInput.inputRight;
            inputBack       = playerInput.inputBack;
            inputConfirm    = playerInput.inputConfirm;
            altInputUp      = playerInput.altInputUp;
            altInputDown    = playerInput.altInputDown;
            altInputLeft    = playerInput.altInputLeft;
            altInputRight   = playerInput.altInputRight;
            altInputBack    = playerInput.altInputBack;
            altInputConfirm = playerInput.altInputConfirm;
        }
        
        public bool RetrieveInputBack(){
            return Input.GetKeyDown(inputBack) || Input.GetKeyDown(altInputBack);
        }

        public bool RetrieveInputConfirm(){
            return Input.GetKeyDown(inputConfirm) || Input.GetKeyDown(altInputConfirm);
        }

        public bool RetrieveInputDown(){
            return Input.GetKeyDown(inputDown) || Input.GetKeyDown(altInputDown);
        }

        public bool RetrieveInputLeft(){
            return Input.GetKeyDown(inputLeft) || Input.GetKeyDown(altInputLeft);
        }

        public bool RetrieveInputRight(){
            return Input.GetKeyDown(inputRight) || Input.GetKeyDown(altInputRight);
        }

        public bool RetrieveInputUp(){
            return Input.GetKeyDown(inputUp) || Input.GetKeyDown(altInputUp);
        }

        public bool RetrieveInputMouseLeft(){
            return Mouse.current.leftButton.wasPressedThisFrame;
        }

        public bool RetrieveInputTouch(){
            if(Touchscreen.current == null){ return false; }
            return Touchscreen.current.press.wasPressedThisFrame;
        }

        public Vector2 RetrieveMousePosition(){
            return Mouse.current.position.ReadValue();
        }

        public Vector2 RetrieveTouchPosition(){
            return Touchscreen.current.position.ReadValue();
        }
    }
}
