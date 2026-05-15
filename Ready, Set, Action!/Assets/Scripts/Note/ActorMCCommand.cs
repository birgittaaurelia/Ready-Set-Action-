using UnityEngine;
using TMPro;

public class ActorMCCommand : MonoBehaviour
{
    public RectTransform timingRectangle;
    public TextMeshProUGUI directionText;
    public float duration = 1.0f;

    [Header ("Hit Result")]
    public GameObject hitResultPrefab;
    public Transform canvasTransform;

    private string targetKey;
    private float elapsed = 0f;
    private bool wasHandled = false;
    private MCCommandSpawner knight;

    public void SetupCommand(MCCommandSpawner knightRef, string requiredKey)
    {
        knight = knightRef;
        targetKey = requiredKey.ToUpper();
        directionText.text = targetKey;
    }

    void Update()
    {
        if (wasHandled) return;

        elapsed += Time.deltaTime;
        float progress = elapsed / duration;
        float currentScale = Mathf.Lerp(3.0f, 1.0f, progress);
        timingRectangle.localScale = new Vector3(currentScale, currentScale, 1);

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(targetKey.ToLower()))
            {
                EvaluateHit();
            }
            else
            {
                Fail();
            }
        }

        if (elapsed > duration + 0.15f)
        {
            Fail();
        }
    }

    void EvaluateHit()
    {
        wasHandled = true;
        float error = Mathf.Abs(duration - elapsed);

        if (error < 0.2f)
        {
            SpawnHitResult("PERFECT", Color.yellow);
            ScoreCalculation.Instance.UpdateScore(ScoreCalculation.Instance.perfectPoints);
            if (knight != null) knight.MoveInDirection(targetKey);
        }
        else if (error < 0.3f)
        {
            SpawnHitResult("GOOD", Color.cyan);
            ScoreCalculation.Instance.UpdateScore(ScoreCalculation.Instance.goodPoints);
            if (knight != null) knight.MoveInDirection(targetKey);
        }
        else
        {
            SpawnHitResult("MISS", Color.gray);
            ScoreCalculation.Instance.UpdateScore(ScoreCalculation.Instance.perfectPoints);
        }

        Destroy(gameObject);
    }

    void SpawnHitResult(string rank, Color color)
    {
        if (hitResultPrefab == null || canvasTransform == null)
        {
            Debug.LogError("ActorMCCommand: hitResultPrefab or canvasTransform is not assigned!");
            return;
        }

        GameObject go = Instantiate(hitResultPrefab, canvasTransform);
        go.transform.SetAsLastSibling();

        RectTransform noteRT = GetComponent<RectTransform>();
        RectTransform resultRT = go.GetComponent<RectTransform>();

        if (resultRT != null && noteRT != null)
        {
            resultRT.anchoredPosition = noteRT.anchoredPosition;
        }

        TextMeshProUGUI txt = go.GetComponentInChildren<TextMeshProUGUI>();
        if (txt != null)
        {
            txt.text = rank;
            txt.color = color;
        }
        else
        {
            Debug.LogWarning("ActorMCCommand: No TextMeshProUGUI found in hitResultPrefab children.");
        }
    }


    void Fail()
    {
        wasHandled = true;
        SpawnHitResult("MISS", Color.gray);
        ScoreCalculation.Instance.UpdateScore(ScoreCalculation.Instance.missPoints);
        Destroy(gameObject);
    }
}
