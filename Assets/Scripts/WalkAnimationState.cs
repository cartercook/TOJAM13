using System.Collections;
using UnityEngine;
using DigitalRuby.Tween;

[RequireComponent(typeof(FrameAnimator))]
public class WalkAnimationState : MonoBehaviour
{
    public float distance = 16;

    public int startFrame;

    public int endFrame;

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
        StartCoroutine(playFrames());

        print($"{rectTransform.offsetMin.x}, {rectTransform.sizeDelta.x}");

        TweenFactory.Tween(
            null,
            characterTransform.position.x,
            rectTransform.offsetMin.x + rectTransform.sizeDelta.x * Random.value,
            1,
            TweenScaleFunctions.QuadraticEaseInOut,
            (ITween<float> t) =>
            {
                Vector3 pos = characterTransform.position;
                pos.x = t.CurrentValue;
                characterTransform.position = pos;
            },
            (t) =>
            {
                enabled = false;
                nextState.enabled = true;
            });
    }

    private void goToNextState()
    {
        enabled = false;
        nextState.enabled = true;
    }

    private IEnumerator playFrames()
    {
        elapsed = 0;

        updateFrame();

        while (elapsed < time)
        {
            yield return null;
            updateFrame();
        }

        enabled = false;
        nextState.enabled = true;
    }

    private void updateFrame()
    {
        int frameIndex = Mathf.Min(
            startFrame + Mathf.FloorToInt(((endFrame + 1) - startFrame) * (elapsed / time)),
            endFrame);

        frameAnimator.frameNumber = frameIndex;
        elapsed += Time.deltaTime;
    }
}
