using System;
using Sirenix.OdinInspector;
using TurnBasedPractice.Animates;
using UnityEngine;

namespace TurnBasedPractice.MainMenu.Commands
{
    [CreateAssetMenu(menuName = "CommandSystem/Command", fileName = "New Command")]
    public class CommandScriptable : ScriptableObject
    {
        [LabelWidth(60)]
        public CommandType Type;
        [LabelWidth(60)]
        public GameObject Target;
        public bool PlayImmediately = false;

        [ShowIf("IsIris"), HideLabel]
        [Header("-- Content --")]
        public IrisInContent IrisInFields;

        [ShowIf("IsPlayAnimation"), HideLabel]
        [Header("-- Content --")]
        public PlayAnimationContent AniFields;
        
        [ShowIf("IsMove"), HideLabel]
        [Header("-- Content --")]
        public MoveContent MoveFields;

        [Toggle("Enable"), LabelText("End Time")]
        public CustomEndTime EndTimeContent;

        private bool IsIris() => Type == CommandType.IrisIn || Type == CommandType.IrisInUi;
        private bool IsPlayAnimation() => Type == CommandType.PlayAnimation;
        private bool IsMove() => Type == CommandType.Move;

        public IrisIn ContentPasteTo(IrisIn irisIn){
            irisIn.Radius = IrisInFields.Radius;
            irisIn.LapCount = IrisInFields.LapCount;
            irisIn.Duration = IrisInFields.Duration;
            irisIn.Fps = IrisInFields.Fps;
            return irisIn;
        }

        public PlayAnimationCommand ContentPasteTo(PlayAnimationCommand animationCommand){
            animationCommand.AnimationName = AniFields.AnimationName;
            return animationCommand;
        }

        public MoveLoopWithBoundsAnimate ContentPasteTo(MoveLoopWithBoundsAnimate moveAnimate){
            moveAnimate.Bounds = MoveFields.Bounds;
            moveAnimate.Speed = MoveFields.Speed;
            moveAnimate.Fps = MoveFields.Fps;
            return moveAnimate;
        }
    }

    [Serializable]
    public class MoveContent
    {
        [LabelWidth(75)]
        public Animates.Bounds Bounds;
        [LabelWidth(75)]
        public float Speed = 1;
        [LabelWidth(75)]
        public float Fps = 60f;
    }

    [Serializable]
    public class IrisInContent
    {
        [LabelWidth(75)]
        public float Radius = 3;
        [LabelWidth(75)]
        public float LapCount = 1;
        [LabelWidth(75)]
        public float Duration = 1;
        [LabelWidth(75)]
        public float Fps = 60f;
    }

    [Serializable]
    public class PlayAnimationContent
    {
        public string AnimationName;
    }

    [Serializable]
    public class CustomEndTime
    {
        public bool Enable;
        public float EndTime;
        [ReadOnly]
        public float Timer = 0;
    }
}
