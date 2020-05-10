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
        Vector2 pos = rectTransform.position;
        pos.y += rectTransform.rect.yMin;

        dialogue.Run(pos, data);
    }
}
