using UnityEngine;
using System.Collections.Generic;

public class MCCommandSpawner : MonoBehaviour
{
    public GameObject directionalCommandPrefab;
    public Transform commandSpawnPoint;
    public Canvas targetCanvas;
    public GameObject hitResultPrefab;

    public List<CommandData> movementTimeline = new List<CommandData>();

    private float stageTimer = 0f;
    private int currentCommandIndex = 0;

    void Update()
    {
        stageTimer += Time.deltaTime;

        if (currentCommandIndex < movementTimeline.Count && stageTimer >= movementTimeline[currentCommandIndex].spawnTime)
        {
            Debug.Log("Spawned!");
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
        }
    }

    public void MoveInDirection(string direction)
    {
        Debug.Log("Knight move: " + direction);

        switch (direction)
        {
            case "W": Debug.Log("W"); break;
            case "A": Debug.Log("A"); break;
            case "S": Debug.Log("S"); break;
            case "D": Debug.Log("D"); break;
        }
    }
}
