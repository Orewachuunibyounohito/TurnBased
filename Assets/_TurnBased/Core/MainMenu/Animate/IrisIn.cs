using System.Collections;
using TurnBasedPractice.MainMenu.Commands;
using UnityEngine;

namespace TurnBasedPractice.Animates
{
    public class IrisIn : MonoBehaviour, IAnimateCommand
    {
        public const float PI = Mathf.PI;

        protected virtual float RADIUS_MULTIPLIER => 1;

        public bool IsRunning{ get; protected set; } = false;

        public float Radius = 3;
        public float LapCount = 1;
        public float Duration = 1;
        public float Fps = 60f;

        protected float currentRadius;
        protected float currentAngle;
        protected Vector2 originPosition;

        void Start(){
            CacheOriginPosition();
        }

        protected virtual void CacheOriginPosition() => originPosition = transform.position;

        public void DoIrisIn() => StartCoroutine(IrisInTask());

        protected virtual IEnumerator IrisInTask()
        {
            if(GetComponent<Animator>()){
                GetComponent<Animator>().Play("FadeIn");
            }
            float radius = Radius * RADIUS_MULTIPLIER;
            currentAngle = PI / 2;
            currentRadius = radius;
            float deltaTime = 1 / Fps;
            float totalAngle = 2 * PI * LapCount;
            float anglePerSec = totalAngle / Duration;
            float radiusPerSec = radius / Duration;
            float stepForAngle = anglePerSec * deltaTime;
            float stepForRadius = radiusPerSec * deltaTime;
            Vector2 newPosition = originPosition + currentRadius * new Vector2(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle));
            UpdatePosition(newPosition);
            while (IsSpinning())
            {
                yield return new WaitForSeconds(deltaTime);
                currentRadius = Mathf.Max(currentRadius - stepForRadius, 0);
                currentAngle += stepForAngle;

                newPosition = originPosition + currentRadius * new Vector2(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle));
                UpdatePosition(newPosition);
            }
            IsRunning = false;
        }

        protected virtual bool IsSpinning() => !transform.position.Equals(originPosition);
        protected virtual void UpdatePosition(Vector2 newPosition) => transform.position = newPosition;

        public void Execute(){
            IsRunning = true;
            DoIrisIn();
        }
        public void Finish() => IsRunning = false;
        public void Skip(){
            StopCoroutine(IrisInTask());
            UpdatePosition(originPosition);
            Finish();
        }
    }
}