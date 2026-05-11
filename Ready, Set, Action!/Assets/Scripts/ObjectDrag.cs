using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 startDragPosition;
    private Vector3 inventoryPosition;
    private Vector3 inventoryScale;
    [SerializeField] private Vector3 worldScale = new Vector3(1f, 1f, 1f);
    private Camera mainCamera;
    private Collider2D col;
    private SpriteRenderer spriteRenderer;
    private bool isInInventory = true;

    private void Start()
    {
        mainCamera = Camera.main;
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 5;
        inventoryScale = transform.localScale;
        inventoryPosition = transform.localPosition;
    }

    private void OnMouseDown()
    {
        startDragPosition = transform.position;

        transform.localScale = worldScale;
        spriteRenderer.sortingOrder = 100;

        transform.position = GetMousePositionInWorldSpace();

        SetAlpha(0.5f);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePositionInWorldSpace();

    }

    private void OnMouseUp()
    {
        col.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        col.enabled = true;

        SetAlpha(1.0f);

        if (hitCollider != null)
        {
            if (hitCollider.TryGetComponent(out IObjectDropArea objectDropArea))
            {
                objectDropArea.OnObjectDrop(this);
                spriteRenderer.sortingOrder = 1;
                isInInventory = false;
            }

            else if (hitCollider.CompareTag("Storage"))
            {
                ReturnToInventory();
            }
            else
            {
                ResetPosition();
            }
        }
        else
        {
            ResetPosition();
        }
    }

    private void ReturnToInventory()
    {
        transform.position = inventoryPosition;
        transform.localScale = inventoryScale;
        spriteRenderer.sortingOrder = 5;
        isInInventory = true;
    }

    private void ResetPosition()
    {
        transform.position = startDragPosition;
        if (isInInventory)
        {
            transform.localScale = inventoryScale;
            spriteRenderer.sortingOrder = 5;
        }
        else
        {
            spriteRenderer.sortingOrder = 1;
        }
    }

    public Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(mainCamera.transform.position.z);
        Vector3 p = mainCamera.ScreenToWorldPoint(mousePos);
        p.z = 0f;
        return p;
    }

    private void SetAlpha(float alphaValue)
    {
        Color tempColor = spriteRenderer.color;
        tempColor.a = alphaValue;
        spriteRenderer.color = tempColor;
    }
}
