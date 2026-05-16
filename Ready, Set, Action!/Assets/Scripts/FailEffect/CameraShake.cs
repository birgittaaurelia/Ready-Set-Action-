using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private Vector3 originalPosition;

    void Awake()
    {
        Instance = this;
        originalPosition = transform.localPosition;
    }

    public void Shake(float duration = 0.3f, float magnitude = 0.2f)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(
                originalPosition.x + x,
                originalPosition.y + y,
                originalPosition.z
            );

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}