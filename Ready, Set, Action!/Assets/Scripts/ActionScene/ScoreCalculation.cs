using UnityEngine;

public class ScoreCalculation : MonoBehaviour
{
    public static ScoreCalculation Instance;
    int score;

    public int perfectPoints = 30;
    public int goodPoints = 10;
    public int missPoints = 0;

    void Awake()
    {
        Instance = this;
    }


    public void UpdateScore(int point)
    {
        score += point;
        Debug.Log(score);
    }
}
