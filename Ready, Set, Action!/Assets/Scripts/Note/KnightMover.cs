using UnityEngine;

public class KnightMover : MonoBehaviour
{
    [Header("Movement")]
    public float moveDistance = 1f; 
    public float moveSpeed = 5f; 

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }

    public void Move(MoveDirection direction)
    {
        if (direction == MoveDirection.None) return;

        Vector3 offset = direction == MoveDirection.Right
            ? Vector3.right * moveDistance
            : Vector3.left * moveDistance;

        targetPosition = transform.position + offset;
        isMoving = true;
    }
}