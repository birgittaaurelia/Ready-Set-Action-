using UnityEngine;

public class SweatEffect : MonoBehaviour
{
    public float duration = 1f;
    public Vector3 offset = new Vector3(0.5f, 1f, -2f);

    private float elapsed = 0f;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
            Debug.LogError("SweatEffect: No SpriteRenderer found!");
    }

    void Start()
    {
        transform.position += offset;
        spriteRenderer.sortingOrder = 10;
    }

    void Update()
    {
        if (spriteRenderer == null) return;

        elapsed += Time.deltaTime;

        float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;

        if (elapsed >= duration)
            Destroy(gameObject);
    }
}