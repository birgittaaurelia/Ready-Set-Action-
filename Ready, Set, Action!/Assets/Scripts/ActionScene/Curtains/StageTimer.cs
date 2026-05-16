using UnityEngine;
using UnityEngine.SceneManagement;

public class StageTimer : MonoBehaviour
{
    [Header("Timing")]
    public float stageDuration = 60f;

    [Header("Scene")]
    public string scoringSceneName = "ScoringMenu";

    private float timer = 0f;
    private bool hasTransitioned = false;

    void Update()
    {
        timer += Time.deltaTime;

        if (!hasTransitioned && timer >= stageDuration)
        {
            hasTransitioned = true;
            TransitionToScoring();
        }
    }

    void TransitionToScoring()
    {
        Debug.Log("Stage complete! Score: " + ScoreCalculation.Instance.score);
        SceneManager.LoadScene(scoringSceneName);
    }
}