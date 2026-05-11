using UnityEngine;

public class RhythmNote : MonoBehaviour
{
    public RectTransform timingRing;
    private float elapsed = 0f;
    private float duration = 1.0f;
    private bool wasHandled = false;

    void Update()
    {
        if (wasHandled) return;

        elapsed += Time.deltaTime;
        float progress = elapsed / duration;

        float currentScale = Mathf.Lerp(3.0f, 1.0f, progress);
        timingRing.localScale = new Vector3(currentScale, currentScale, 1);

        if (elapsed > duration + 0.15f)
        {
            Fail();
        }
    }

    public void OnClick()
    {
        if (wasHandled) return;

        float error = Mathf.Abs(duration - elapsed);

        if (error < 0.1f)
        {
            Debug.Log("Perfect Hit!");
        }
        else
        {
            Debug.Log("Too Early!");
        }

        wasHandled = true;
        Destroy(gameObject, 0.1f);
    }

    void Fail()
    {
        Debug.Log("Missed!");
        wasHandled = true;
        Destroy(gameObject);
    }
}
