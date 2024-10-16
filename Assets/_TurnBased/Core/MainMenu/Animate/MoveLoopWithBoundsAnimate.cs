using System;
using System.Collections;
using TurnBasedPractice.MainMenu.Commands;
using UnityEngine;

namespace TurnBasedPractice.Animates
{
    public class MoveLoopWithBoundsAnimate : MonoBehaviour, IAnimateCommand
    {   
        public bool IsRunning{ get; protected set; } = false;

        public float Speed = 3;
        public float Duration = 1;
        public float Fps = 60f;
        public Bounds Bounds = new Bounds{
            Left = -3,
            Right = 3
        };

        public void DoMove() => StartCoroutine(MoveTask());

        protected virtual IEnumerator MoveTask()
        {
            IsRunning = false;
            float deltaTime = Fps == 0? Time.deltaTime : 1/Fps;
            float step = Speed * deltaTime;
            if(GetComponent<Animator>()){
                GetComponent<Animator>().Play("run-Animation");
            }

            var newPosition = transform.position.AsVector2() + Vector2.right*step;
            UpdatePosition(newPosition);
            while (true){
                yield return new WaitForSeconds(deltaTime);

                newPosition = transform.position.AsVector2() + Vector2.right*step;
                newPosition = AdjustPositionWithBounds(newPosition);
                UpdatePosition(newPosition);
            }
        }

        private Vector2 AdjustPositionWithBounds(Vector2 newPosition){
            float characterWidth = GetComponent<SpriteRenderer>().size.x;
            Vector2 forefootPosition = newPosition + Vector2.right * characterWidth/2;
            Vector2 heelPosition = newPosition + Vector2.left * characterWidth/2;
            if(heelPosition.x > Bounds.Right){
                return new Vector2(Bounds.Left - characterWidth/2, newPosition.y);
            }
            if(forefootPosition.x < Bounds.Left){
                return new Vector2(Bounds.Right + characterWidth/2, newPosition.y);
            }
            return newPosition;
        }

        protected virtual void UpdatePosition(Vector2 newPosition) => transform.position = newPosition;

        public void Execute(){
            IsRunning = true;
            DoMove();
        }
        public void Finish() => IsRunning = false;
        public void Skip(){
            StopCoroutine(MoveTask());
            Finish();
        }
    }

    [Serializable]
    public class Bounds
    {
        public float Left;
        public float Right;
        public float Top;
        public float Bottom;
    }
}