using UnityEngine;

public class Storage : MonoBehaviour, IObjectDropArea
{
    public void OnObjectDrop(ObjectDrag prop)
    {
        prop.transform.position = transform.position;

        if (StageDataManager.Instance != null)
            StageDataManager.Instance.UnregisterProp(prop);
    }
}