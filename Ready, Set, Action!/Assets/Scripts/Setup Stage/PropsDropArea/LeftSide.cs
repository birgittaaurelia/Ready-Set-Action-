using UnityEngine;

public class LeftSide : MonoBehaviour, IObjectDropArea
{
    public void OnObjectDrop(ObjectDrag prop)
    {
        prop.transform.position = transform.position;

        if (StageDataManager.Instance != null)
            StageDataManager.Instance.RegisterProp(prop, transform.position);
        else
            Debug.LogError("LeftSide: No StageDataManager found in scene!");
    }
}