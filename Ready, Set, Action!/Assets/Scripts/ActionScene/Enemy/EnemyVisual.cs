using UnityEngine;

public class EnemyVisual : MonoBehaviour
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
            Debug.LogError("EnemyVisual: No SpriteRenderer found on Enemy!");
            return;
        }

        SetIdle();
    }

    public void SetPose(Sprite pose)
    {
        if (pose == null)
        {
            Debug.LogWarning("EnemyVisual: No pose sprite assigned for this note!");
            return;
        }

        spriteRenderer.sprite = pose;
    }

    public void SetIdle()
    {
        if (idleSprite != null)
            spriteRenderer.sprite = idleSprite;
    }

    public void PlaySweatEffect()
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
    }

    public void PlayCameraShake()
    {
        if (CameraShake.Instance != null)
            CameraShake.Instance.Shake(0.3f, 0.2f);
        else
            Debug.LogWarning("KnightVisual: No CameraShake found on camera!");
    }
}