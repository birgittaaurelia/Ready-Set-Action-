using UnityEngine;
using System.Collections.Generic;

public class StagePhaseManager : MonoBehaviour
{
    public static StagePhaseManager Instance;

    [Header("References")]
    public KnightVisual knightVisual;
    public Transform knightTransform;
    public EnemyVisual enemyVisual;
    public Transform enemyTransform;

    [Header("Phases")]
    public List<StagePhaseData> phases = new List<StagePhaseData>();

    private float stageTimer = 0f;
    private int currentPhaseIndex = 0;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        stageTimer += Time.deltaTime;

        if (currentPhaseIndex < phases.Count)
        {
            if (stageTimer >= phases[currentPhaseIndex].triggerTime)
            {
                ApplyPhase(phases[currentPhaseIndex]);
                currentPhaseIndex++;
            }
        }
    }

    void ApplyPhase(StagePhaseData phase)
    {
        if (knightVisual == null)
            Debug.LogError("StagePhaseManager: knightVisual is null! Assign it in Inspector.");
        else if (phase.knightSprite == null)
            Debug.LogWarning("StagePhaseManager: knightSprite is null in this phase entry!");
        else
        {
            knightVisual.idleSprite = phase.knightSprite;
            knightVisual.SetIdle();
            Debug.Log("Knight sprite changed to: " + phase.knightSprite.name);
        }

        if (knightTransform == null)
            Debug.LogError("StagePhaseManager: knightTransform is null! Assign it in Inspector.");
        else
        {
            knightTransform.position = phase.knightPosition;
            Debug.Log("Knight position changed to: " + phase.knightPosition);
        }

        // Check enemy
        if (enemyVisual == null)
            Debug.LogError("StagePhaseManager: enemyVisual is null! Assign it in Inspector.");
        else if (phase.enemySprite == null)
            Debug.LogWarning("StagePhaseManager: enemySprite is null in this phase entry!");
        else
        {
            enemyVisual.idleSprite = phase.enemySprite;
            enemyVisual.SetIdle();
            Debug.Log("Enemy sprite changed to: " + phase.enemySprite.name);
        }

        if (enemyTransform == null)
            Debug.LogError("StagePhaseManager: enemyTransform is null! Assign it in Inspector.");
        else
        {
            enemyTransform.position = phase.enemyPosition;
            Debug.Log("Enemy position changed to: " + phase.enemyPosition);
        }
    }
}