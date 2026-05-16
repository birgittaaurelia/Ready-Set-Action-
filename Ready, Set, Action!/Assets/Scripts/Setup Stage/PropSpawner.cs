using UnityEngine;
using System.Collections.Generic;

public class PropSpawner : MonoBehaviour
{
    public List<GameObject> propPrefabs;

    void Start()
    {
        if (StageDataManager.Instance == null)
        {
            Debug.LogError("PropSpawner: No StageDataManager found!");
            return;
        }

        Debug.Log("PropSpawner: Spawning " + StageDataManager.Instance.placedProps.Count + " props");

        foreach (PropData data in StageDataManager.Instance.placedProps)
        {
            SpawnProp(data);
        }
    }

    void SpawnProp(PropData data)
    {
        GameObject prefab = propPrefabs.Find(p => p.name == data.propName);

        if (prefab == null)
        {
            Debug.LogWarning("PropSpawner: No prefab found for: " + data.propName);
            return;
        }

        Vector3 worldPos = Camera.main.ViewportToWorldPoint(new Vector3(
            data.normalizedPosition.x,
            data.normalizedPosition.y,
            Mathf.Abs(Camera.main.transform.position.z)
        ));
        worldPos.z = 0f;

        GameObject prop = Instantiate(prefab, worldPos, Quaternion.identity);
        Debug.Log("Spawned: " + data.propName + " at world pos: " + worldPos);
    }
}