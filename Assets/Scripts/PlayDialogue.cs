using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class PlayDialogue : MonoBehaviour
{
    public AnimateDialogue dialogue;

    public CharacterDialogueData data;

    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Play()
    {
        if (!data)
        {
            return;
        }

        Vector2 pos = rectTransform.position;

        dialogue.Run(pos, data);
    }
}
