using System.Collections;
using UnityEngine;
using DigitalRuby.Tween;

[RequireComponent(typeof(FrameAnimator))]
public class WalkAnimationState : MonoBehaviour
{
    public float distance = 16;

    public int leftStartFrame;
    public int leftEndFrame;

    public int rightStartFrame;
    public int rightEndFrame;

    public float time = 1;

    private float elapsed = 0;

    public MonoBehaviour nextState;

    private FrameAnimator frameAnimator;

    private RectTransform rectTransform;

    private RectTransform characterTransform;

    void Awake()
    {
        frameAnimator = GetComponent<FrameAnimator>();
        rectTransform = GetComponentInParent<Room>().GetComponent<RectTransform>();
        characterTransform = GetComponentInParent<CharacterRoot>().GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        // print($"{rectTransform.gameObject.name}: {rectTransform.localPosition.x}, {rectTransform.rect.xMin}, {rectTransform.sizeDelta.x}");
        float x = characterTransform.localPosition.x;
        float targetX = rectTransform.rect.xMin + rectTransform.sizeDelta.x * Random.value;

        TweenFactory.Tween(
            null,
            x,
            targetX,
            1,
            TweenScaleFunctions.QuadraticEaseInOut,
            (ITween<float> t) =>
            {
                Vector3 pos = characterTransform.localPosition;
                pos.x = t.CurrentValue;
                characterTransform.localPosition = pos;
            },
            (t) => goToNextState());

        StartCoroutine(targetX < x
            ? playFrames(leftStartFrame, leftEndFrame)
            : playFrames(rightStartFrame, rightEndFrame));
    }

    private void goToNextState()
    {
        enabled = false;
        nextState.enabled = true;
    }

    private IEnumerator playFrames(int startFrame, int endFrame)
    {
        elapsed = 0;

        updateFrame(startFrame, endFrame);

        while (elapsed < time)
        {
            yield return null;
            updateFrame(startFrame, endFrame);
        }

        enabled = false;
        nextState.enabled = true;
    }

    private void updateFrame(int startFrame, int endFrame)
    {
        int frameIndex = Mathf.Min(
            startFrame + Mathf.FloorToInt(((endFrame + 1) - leftStartFrame) * (elapsed / time)),
            endFrame);

        frameAnimator.frameNumber = frameIndex;
        elapsed += Time.deltaTime;
    }
}
