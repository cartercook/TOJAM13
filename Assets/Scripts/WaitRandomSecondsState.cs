using System.Collections;
using UnityEngine;

public class WaitRandomSecondsState : MonoBehaviour
{
    public MonoBehaviour nextState;

    void OnEnable()
    {
        StartCoroutine(waitRandomTime());
    }

    private IEnumerator waitRandomTime()
    {
        yield return new WaitForSeconds(Random.Range(3f, 15f));
        enabled = false;
        nextState.enabled = true;
    }
}
