using UnityEngine;

/// <summary>
/// Represents a reflective mirror that can be rotated by the player
/// </summary>
[RequireComponent(typeof(Collider))]
public class Mirror : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private bool canRotate = true;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private bool lockXRotation = true;
    [SerializeField] private bool lockYRotation = false;
    [SerializeField] private bool lockZRotation = true;
    
    [Header("Visual Settings")]
    [SerializeField] private Color mirrorColor = new Color(0.7f, 0.9f, 1f, 0.5f);
    
    private bool isDragging = false;
    private Vector3 lastMousePosition;
    private Renderer mirrorRenderer;
    
    void Start()
    {
        // Ensure this object has the Mirror tag
        if (!gameObject.CompareTag("Mirror"))
        {
            gameObject.tag = "Mirror";
        }
        
        // Setup visual appearance
        mirrorRenderer = GetComponent<Renderer>();
        if (mirrorRenderer != null && mirrorRenderer.material != null)
        {
            mirrorRenderer.material.color = mirrorColor;
        }
    }
    
    void Update()
    {
        if (!canRotate) return;
        
        HandleMouseInput();
    }
    
    void HandleMouseInput()
    {
        // Check for mouse click on this mirror
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                    lastMousePosition = Input.mousePosition;
                }
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        
        // Rotate mirror while dragging
        if (isDragging)
        {
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
            
            float rotationX = lockXRotation ? 0 : -mouseDelta.y * rotationSpeed * Time.deltaTime;
            float rotationY = lockYRotation ? 0 : mouseDelta.x * rotationSpeed * Time.deltaTime;
            float rotationZ = lockZRotation ? 0 : mouseDelta.x * rotationSpeed * Time.deltaTime;
            
            transform.Rotate(rotationX, rotationY, rotationZ, Space.World);
            
            lastMousePosition = Input.mousePosition;
        }
    }
    
    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
    
    public void SetRotatable(bool rotatable)
    {
        canRotate = rotatable;
    }
    
    void OnDrawGizmos()
    {
        // Draw normal direction in editor
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, transform.forward * 1f);
    }
}
