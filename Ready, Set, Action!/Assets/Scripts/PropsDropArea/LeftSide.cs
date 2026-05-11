using UnityEngine;

public class LeftSide : MonoBehaviour, IObjectDropArea
{
    public void OnObjectDrop(ObjectDrag prop)
    {
        prop.transform.position = transform.position;
    }
}
