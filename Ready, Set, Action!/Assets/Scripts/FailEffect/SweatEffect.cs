using UnityEngine;

public class SweatEffect : MonoBehaviour
{
    public float duration = 1f;
    public Vector3 offset = new Vector3(0.5f, 1f, 0f);

    private float elapsed = 0f;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        elapsed += Time.deltaTime;

        float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;

        if (elapsed >= duration)
            Destroy(gameObject);
    }
}