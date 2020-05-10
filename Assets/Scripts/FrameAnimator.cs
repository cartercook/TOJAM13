using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FrameAnimator : MonoBehaviour
{
    public int frameNumber = 78;

    [SerializeField]
    private Texture2D texture;

    private new SpriteRenderer renderer;

    private Sprite[] sprites;

    private RectTransform roomTranform;

    private Animator animator;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>($"Spritesheets/{texture.name}");

        roomTranform = GetComponentInParent<Room>().GetComponent<RectTransform>();
        if (!roomTranform)
        {
            Debug.LogError("FrameAnimator must be decendant of a Room");
        }
    }

    void Update()
    {
        /// animator controls this value
        renderer.sprite = sprites[frameNumber];
    }
}
