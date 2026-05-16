using UnityEngine;
using System.Collections.Generic;

public class StageDataManager : MonoBehaviour
{
    public static StageDataManager Instance;
    public List<PropData> placedProps = new List<PropData>();

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

    public void RegisterProp(ObjectDrag prop, Vector3 worldPosition)
    {
        Vector2 normalized = Camera.main.WorldToViewportPoint(worldPosition);

        string propName = prop.gameObject.name.Replace("(Clone)", "").Trim();
        PropData existing = placedProps.Find(p => p.propName == propName);

        if (existing != null)
        {
            existing.worldPosition = worldPosition;
            existing.normalizedPosition = normalized;
        }
        else
        {
            placedProps.Add(new PropData
            {
                propName = propName,
                worldPosition = worldPosition,
                normalizedPosition = normalized
            });
        }

        Debug.Log("Saved: " + propName + " normalized: " + normalized);
    }

    public void UnregisterProp(ObjectDrag prop)
    {
        string propName = prop.gameObject.name.Replace("(Clone)", "").Trim();
        placedProps.RemoveAll(p => p.propName == propName);
    }

    public void ClearProps()
    {
        placedProps.Clear();
    }
}