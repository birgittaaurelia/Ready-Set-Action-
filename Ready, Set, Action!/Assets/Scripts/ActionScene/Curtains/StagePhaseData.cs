using UnityEngine;

[System.Serializable]
public class StagePhaseData
{
    public float triggerTime;

    [Header("Knight")]
    public Sprite knightSprite;
    public Vector3 knightPosition;

    [Header("Enemy")]
    public Sprite enemySprite;
    public Vector3 enemyPosition;
}