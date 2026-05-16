using UnityEngine;

public class KnightVisual : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite idleSprite;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("KnightVisual: No SpriteRenderer found on Knight!");
            return;
        }

        SetIdle();
    }

    public void SetPose(Sprite pose)
    {
        if (pose == null)
        {
            Debug.LogWarning("KnightVisual: No pose sprite assigned for this command!");
            return;
        }
        spriteRenderer.sprite = pose;
    }

    public void SetIdle()
    {
        if (idleSprite != null)
            spriteRenderer.sprite = idleSprite;
    }
}