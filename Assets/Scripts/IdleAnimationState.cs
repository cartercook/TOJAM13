using UnityEngine;

[RequireComponent(typeof(FrameAnimator))]
public class IdleAnimationState : MonoBehaviour
{
    public MonoBehaviour nextState;

    private FrameAnimator frameAnimator;

    void Awake()
    {
        frameAnimator = GetComponent<FrameAnimator>();
    }

    void OnEnable()
    {
        // idle
        frameAnimator.frameNumber = 79;

        // next state
        enabled = false;
        nextState.enabled = true;
    }
}
