using UnityEngine;

public class TrashBin : MonoBehaviour, IObjectDropArea
{
    public void OnObjectDrop(ObjectDrag prop)
    {
        if (StageDataManager.Instance != null)
            StageDataManager.Instance.UnregisterProp(prop);

        Destroy(prop.gameObject);
    }
}