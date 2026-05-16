using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoringMenu : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI scoreText;

    [Header("Stars")]
    public Image[] stars;          // drag 5 star Image components here in order
    public Sprite fullStar;        // full star sprite
    public Sprite emptyStar;       // empty star sprite

    [Header("Star Thresholds")]
    public int oneStarScore = 100;
    public int twoStarScore = 200;
    public int threeStarScore = 300;
    public int fourStarScore = 400;
    public int fiveStarScore = 500;

    void Start()
    {
        if (ScoreCalculation.Instance == null)
        {
            Debug.LogError("ScoringMenu: No ScoreCalculation found!");
            return;
        }

        int finalScore = ScoreCalculation.Instance.score;

        // Display score
        scoreText.text = finalScore.ToString();

        // Display stars
        int earnedStars = GetStarCount(finalScore);
        UpdateStars(earnedStars);

        // Reset for next playthrough
        ScoreCalculation.Instance.ResetScore();
    }

    int GetStarCount(int finalScore)
    {
        if (finalScore >= fiveStarScore)  return 5;
        if (finalScore >= fourStarScore)  return 4;
        if (finalScore >= threeStarScore) return 3;
        if (finalScore >= twoStarScore)   return 2;
        if (finalScore >= oneStarScore)   return 1;
        return 0;
    }

    void UpdateStars(int earnedStars)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            if (stars[i] == null)
            {
                Debug.LogWarning("ScoringMenu: Star " + i + " is not assigned!");
                continue;
            }

            stars[i].sprite = i < earnedStars ? fullStar : emptyStar;
        }
    }
}