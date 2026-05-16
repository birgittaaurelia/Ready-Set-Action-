using UnityEngine;
using System.Collections.Generic;

public class MCCommandSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject directionalCommandPrefab;
    public GameObject hitResultPrefab;
    public Canvas targetCanvas;

    [Header("Knight References")]
    public Transform commandSpawnPoint;
    public AudioSource audioSource;
    public KnightVisual knightVisual;

    public List<CommandData> movementTimeline = new List<CommandData>();

    private float stageTimer = 0f;
    private int currentCommandIndex = 0;

    void Awake()
    {
        if (targetCanvas == null)
        {
            targetCanvas = FindFirstObjectByType<Canvas>();
            if (targetCanvas == null)
                Debug.LogError("MCCommandSpawner: No Canvas found in scene!");
        }
    }

    void Update()
    {
        stageTimer += Time.deltaTime;

        if (currentCommandIndex < movementTimeline.Count &&
            stageTimer >= movementTimeline[currentCommandIndex].spawnTime)
        {
            SpawnDirectionPrompt(movementTimeline[currentCommandIndex]);
            currentCommandIndex++;
        }
    }

    void SpawnDirectionPrompt(CommandData command)
    {
        GameObject commandObj = Instantiate(directionalCommandPrefab, targetCanvas.transform);

        RectTransform rt = commandObj.GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.position = commandSpawnPoint.position;
            rt.localRotation = Quaternion.identity;
            rt.localScale = Vector3.one;
        }

        ActorMCCommand commandScript = commandObj.GetComponent<ActorMCCommand>();
        if (commandScript != null)
        {
            commandScript.SetupCommand(this, command.commandKey);
            commandScript.hitResultPrefab = hitResultPrefab;
            commandScript.canvasTransform = targetCanvas.transform;
            commandScript.SetDuration(command.duration);
            commandScript.commandData = command;
        }
        else
        {
            Debug.LogWarning("MCCommandSpawner: directionalCommandPrefab is missing ActorMCCommand!");
        }
    }

    public void ExecuteCommand(CommandData data)
    {
        if (data == null) return;

        if (knightVisual != null)
            knightVisual.SetPose(data.poseSprite);

        if (audioSource != null && data.soundEffect != null)
            audioSource.PlayOneShot(data.soundEffect);

        if (!string.IsNullOrEmpty(data.stageEffectTag))
        {
            GameObject stageEffect = GameObject.FindWithTag(data.stageEffectTag);
            if (stageEffect != null)
                stageEffect.SendMessage("TriggerEffect", SendMessageOptions.DontRequireReceiver);
        }

        Debug.Log("Knight executed: " + data.commandKey);
    }
}