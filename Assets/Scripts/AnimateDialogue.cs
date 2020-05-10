﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(AudioSource))]
public class AnimateDialogue : MonoBehaviour
{
    public AudioClip[] soundClips;

    private int lineNum = 0;

    private int charNum = 0;

    private Text text;

    private AudioSource audio;

    private Coroutine coroutine;

    private Transform rootTransform;

    private CharacterDialogueData dialogue;

    private string[] lines
    {
        get { return dialogue.lines[dialogue.index].array; }
    }

    void Start()
    {
        text = GetComponent<Text>();
        audio = GetComponent<AudioSource>();
        rootTransform = GetComponentInParent<TextboxRoot>().transform;
    }

    public void Run(Vector2 position, CharacterDialogueData dialogue)
    {
        rootTransform.position = position;
        this.dialogue = dialogue;

        gameObject.SetActive(true);

        coroutine = StartCoroutine(printChar());
    }

    public void Stop()
    {
        StopCoroutine(coroutine);
    }

    /// A coroutine that animates text in a textbox
    private IEnumerator printChar()
    {
        lineNum = 0;

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

        gameObject.SetActive(false);
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
