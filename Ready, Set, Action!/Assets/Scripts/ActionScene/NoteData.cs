using UnityEngine;

[System.Serializable]
public class NoteData
{
    public float spawnTime;
    public Vector2 position;
    public float duration = 1.0f;

    [Header("Enemy Effects")]
    public Sprite enemyPoseSprite;
    public AudioClip soundEffect;
    public string stageEffectTag;
}