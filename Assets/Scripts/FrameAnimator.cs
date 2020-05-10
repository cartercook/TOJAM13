using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FrameAnimator : MonoBehaviour
{
    public int frameNumber;

    [SerializeField]
    private Texture2D texture;

    private new SpriteRenderer renderer;

    private Sprite[] sprites;

    private RectTransform roomTranform;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>($"Spritesheets/{texture.name}");
        print(texture.name);
        print(sprites);
        print(sprites[0]);

        roomTranform = GetComponentInParent<Room>().GetComponent<RectTransform>();

        if (!roomTranform)
        {
            Debug.LogError("FrameAnimator must be decendant of a Room");
        }
    }

    void Update()
    {
        print(sprites);
        print(sprites.Length);
        renderer.sprite = sprites[frameNumber];
    }
}
