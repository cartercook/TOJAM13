using UnityEngine;
using DigitalRuby.Tween;

public class KiteTween : MonoBehaviour
{
    Vector2 startPos;

    void Start()
    {
        startPos = transform.position;
        doTween();
    }

    private void doTween()
    {
        TweenFactory.Tween(
            null,
            transform.position,
            startPos + Random.insideUnitCircle * 4,
            Random.Range(3, 5),
            TweenScaleFunctions.SineEaseInOut,
            (ITween<Vector2> t) => transform.position = t.CurrentValue,
            (t) => doTween());
    }
}
