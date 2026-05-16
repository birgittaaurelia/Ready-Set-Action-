using UnityEngine;

public class KnightVisual : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite idleSprite;

    [Header("Failure Effects")]
    public GameObject sweatPrefab;
    public Vector3 sweatOffset = new Vector3(0.5f, 1f, 0f);
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
            Debug.LogWarning("KnightVisual: No pose sprite assigned!");
            return;
        }

        spriteRenderer.sprite = pose;
    }

    public void SetIdle()
    {
        if (idleSprite != null)
            spriteRenderer.sprite = idleSprite;
    }

    public void PlayFailureEffects()
    {
        if (sweatPrefab != null)
        {
            Vector3 spawnPos = transform.position + sweatOffset;
            Instantiate(sweatPrefab, spawnPos, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("KnightVisual: No sweat prefab assigned!");
        }

        // Shake camera
        if (CameraShake.Instance != null)
            CameraShake.Instance.Shake(0.3f, 0.2f);
        else
            Debug.LogWarning("KnightVisual: No CameraShake found on camera!");
    }
}