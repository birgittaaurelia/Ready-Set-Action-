using UnityEngine;
using TMPro;

public class RhythmNote : MonoBehaviour
{
    public RectTransform timingRing;

    [Header("UI Settings")]
    public GameObject hitResultPrefab;
    public Transform canvasTransform;

    [HideInInspector] public NoteData noteData;
    [HideInInspector] public EnemyVisual enemyVisual;

    private float elapsed = 0f;
    private float duration = 1.0f;
    private bool wasHandled = false;

    public void SetDuration(float newDuration)
    {
        duration = newDuration;
    }

    void Update()
    {
        if (wasHandled) return;

        elapsed += Time.deltaTime;
        float progress = elapsed / duration;

        float currentScale = Mathf.Lerp(3.0f, 1.0f, progress);
        timingRing.localScale = new Vector3(currentScale, currentScale, 1);

        if (elapsed > duration + 0.15f)
            Fail();
    }

    public void OnClick()
    {
        if (wasHandled) return;

        float error = Mathf.Abs(duration - elapsed);
        wasHandled = true;

        if (error < 0.1f)
        {
            SpawnHitResult("PERFECT", Color.yellow);
            ScoreCalculation.Instance.UpdateScore(ScoreCalculation.Instance.perfectPoints);
            ExecuteEnemyEffect("PERFECT");
        }
        else if (error < 0.3f)
        {
            SpawnHitResult("GOOD", Color.cyan);
            ScoreCalculation.Instance.UpdateScore(ScoreCalculation.Instance.goodPoints);
            ExecuteEnemyEffect("GOOD");
        }
        else
        {
            SpawnHitResult("MISS", Color.gray);
            ScoreCalculation.Instance.UpdateScore(ScoreCalculation.Instance.missPoints);
            ExecuteEnemyEffect("MISS");
        }

        Animator anim = GetComponent<Animator>();
        if (anim != null)
        {
            anim.Play("HitShow");
            Destroy(gameObject, 0.2f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void ExecuteEnemyEffect(string hitRank)
    {
        if (enemyVisual == null)
        {
            Debug.LogError("RhythmNote: enemyVisual is null! Not injected from NoteSpawner.");
            return;
        }

        if (noteData == null)
        {
            Debug.LogError("RhythmNote: noteData is null!");
            return;
        }

        enemyVisual.SetPose(noteData.enemyPoseSprite);

        if (hitRank == "GOOD")
        {
            enemyVisual.PlaySweatEffect();
        }
        else if (hitRank == "MISS")
        {
            enemyVisual.PlaySweatEffect();
            enemyVisual.PlayCameraShake();
        }

    }
    void Fail()
    {
        wasHandled = true;
        SpawnHitResult("MISS", Color.gray);
        ScoreCalculation.Instance.UpdateScore(ScoreCalculation.Instance.missPoints);
        ExecuteEnemyEffect("MISS");
        Destroy(gameObject);
    }

    void SpawnHitResult(string rank, Color color)
    {
        if (hitResultPrefab == null || canvasTransform == null)
        {
            Debug.LogError("RhythmNote: hitResultPrefab or canvasTransform is not assigned!");
            return;
        }

        GameObject go = Instantiate(hitResultPrefab, canvasTransform);
        go.transform.SetAsLastSibling();

        RectTransform noteRT = GetComponent<RectTransform>();
        RectTransform resultRT = go.GetComponent<RectTransform>();

        if (resultRT != null && noteRT != null)
            resultRT.anchoredPosition = noteRT.anchoredPosition;

        TextMeshProUGUI txt = go.GetComponentInChildren<TextMeshProUGUI>();
        if (txt != null)
        {
            txt.text = rank;
            txt.color = color;
        }
        else
        {
            Debug.LogWarning("RhythmNote: No TextMeshProUGUI found in hitResultPrefab.");
        }
    }
}