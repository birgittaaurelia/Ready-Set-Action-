using UnityEngine;

[System.Serializable]
public class CommandData
{
    public float spawnTime;
    public string commandKey;
    public float duration = 1.0f;

    [Header("Effects on Success")]
    public string animationTrigger;
    public AudioClip soundEffect;
    public string stageEffectTag;
    public Sprite poseSprite;
}