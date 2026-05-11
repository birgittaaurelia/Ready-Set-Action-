using UnityEngine;

public class TrashBin : MonoBehaviour, IObjectDropArea
{
    public void OnObjectDrop(ObjectDrag prop)
    {
        Destroy(prop.gameObject);
    }
}
