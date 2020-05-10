using UnityEngine;

public class RandomStateChoice : MonoBehaviour
{
    public MonoBehaviour state1;

    public MonoBehaviour state2;

    private void OnEnable()
    {
        enabled = false;
        (Random.value > 0.5 ? state1 : state2).enabled = true;

    }
}
