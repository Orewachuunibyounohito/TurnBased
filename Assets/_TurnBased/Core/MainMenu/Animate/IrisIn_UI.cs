using UnityEngine;

namespace TurnBasedPractice.Animates
{
    public class IrisIn_UI : IrisIn
    {
        protected override float RADIUS_MULTIPLIER => 75;

        protected override void CacheOriginPosition() => originPosition = GetComponent<RectTransform>().anchoredPosition;
        protected override bool IsSpinning() => !GetComponent<RectTransform>().anchoredPosition.Equals(originPosition);
        protected override void UpdatePosition(Vector2 newPosition) => GetComponent<RectTransform>().anchoredPosition = newPosition;
    }
}
