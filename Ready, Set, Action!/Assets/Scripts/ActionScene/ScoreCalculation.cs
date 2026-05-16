using UnityEngine;

public class ScoreCalculation : MonoBehaviour
{
    public static ScoreCalculation Instance;
    public int score;
    public int perfectPoints = 30;
    public int goodPoints = 10;
    public int missPoints = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateScore(int point)
    {
        score += point;
        Debug.Log("Score: " + score);
    }

    public void ResetScore()
    {
        score = 0;
    }
}