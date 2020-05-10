using System.Collections;
using UnityEngine;

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

    void Awake()
    {
        frameAnimator = GetComponent<FrameAnimator>();
        rectTransform = GetComponentInParent<Room>().GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        StartCoroutine(playFrames());
    }

    private IEnumerator playFrames()
    {
        elapsed = 0;

        updateFrame();

        while (elapsed < time)
        {
            print(elapsed + ", " + time);
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

    void Update()
    {
        Vector3 pos = rectTransform.position;
        pos.x += (distance / time) * Time.deltaTime;
        rectTransform.position = pos;
    }
}
