using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(AudioSource))]
public class AnimateDialogue : MonoBehaviour
{
    public string[] lines;

    public AudioClip[] soundClips;

    private int lineNum = 0;

    private int charNum = 0;

    private Text text;

    private AudioSource audio;

    private Coroutine coroutine;

    void Start()
    {
        text = GetComponent<Text>();
        audio = GetComponent<AudioSource>();

        Run();
    }

    public void Run()
    {
        coroutine = StartCoroutine(printChar());
    }

    public void Stop()
    {
        StopCoroutine(coroutine);
    }

    /// A coroutine that animates text in a textbox
    private IEnumerator printChar()
    {
        yield return setTextAndWait();

        while (true)
        {
            charNum++;

            if (charNum >= lines[lineNum].Length)
            {
                lineNum++;
                charNum = 0;

                if (lineNum >= lines.Length)
                {
                    break;
                }

                // idle until mousedown
                while (!Input.GetMouseButtonDown(0))
                {
                    yield return null;
                }
            }

            yield return setTextAndWait();
        }

        lineNum = 0;
    }

    /// Updates the text component and waits for a fraction of a second
    private WaitForSeconds setTextAndWait() {
        if (char.IsLetterOrDigit(lines[lineNum][charNum])) {
            audio.clip = soundClips[Random.Range(0, soundClips.Length - 1)];
            audio.Play();
        }

        text.text = lines[lineNum].Substring(0, charNum + 1);
        return new WaitForSeconds(0.05f);
    }
}
