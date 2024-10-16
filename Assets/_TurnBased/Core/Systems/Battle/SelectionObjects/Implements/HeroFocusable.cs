using TurnBasedPractice.Character;
using TurnBasedPractice.Character.Controller;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace TurnBasedPractice.BattleCore.Selection
{
    public class HeroFocusable : IFocusable
    {   
        public  Hero     Hero{ get; private set; }
        private EventTrigger trigger;
        private Animator heroAnimator;
        private Animator focusableAnimator;

        public ICustomAction UsedAction{ get; set; }
        public EventTrigger.TriggerEvent Clicked;


        public HeroFocusable(Hero hero)
        {
            Hero = hero;
            // heroAnimator = Hero.GetComponent<Animator>() ?? null;
            Transform focusableObject = Hero.transform.Find("Focusable");
            focusableAnimator = focusableObject.GetComponent<Animator>() ?? null;
            Transform spriteObject = focusableObject.Find("Sprite&Mask");
            trigger = spriteObject.GetComponent<EventTrigger>();
            AddClickedEvent();
        }

        private void AddClickedEvent(){
            EventTrigger.Entry entry = new EventTrigger.Entry(){
                eventID = EventTriggerType.PointerClick
            };
            trigger.triggers.Add(entry);
            Clicked = entry.callback;
        }

        public void Interact(PlayerController playerController){
            Debug.Log($"Interact {heroAnimator.name}");
        }
        public void Interact(){
            Debug.Log($"Interact {heroAnimator.name}");
        }

        public void OnFocusEnter(){
            heroAnimator?.Play("Selected");
            focusableAnimator?.Play("Selected_Ui");
        }
        
        public void OnFocusClick(){
            
        }

        public void OnFocusExit(){
            heroAnimator?.Play("None");
            focusableAnimator?.Play("None");
        }
    }

    public class MyEventData : BaseEventData
    {
        public IFocusable focusable;

        public MyEventData(EventSystem eventSystem) : base(eventSystem)
        {}

        public override void Use()
        {
            base.Use();

        }
    }
}