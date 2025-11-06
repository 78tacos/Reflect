using UnityEngine;

/// <summary>
/// Simple camera controller for adjusting view in the game
/// Optional script for better camera control
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("Zoom Settings")]
    [SerializeField] private bool enableZoom = true;
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float minZoom = 3f;
    [SerializeField] private float maxZoom = 20f;
    
    [Header("Pan Settings")]
    [SerializeField] private bool enablePan = true;
    [SerializeField] private float panSpeed = 0.1f;
    [SerializeField] private KeyCode panKey = KeyCode.Space;
    
    [Header("Rotation Settings")]
    [SerializeField] private bool enableRotation = false;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private KeyCode rotateKey = KeyCode.LeftControl;
    
    private Camera mainCamera;
    private Vector3 lastPanPosition;
    private bool isPanning = false;
    private bool isRotating = false;
    
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }
    
    void Update()
    {
        if (enableZoom)
        {
            HandleZoom();
        }
        
        if (enablePan)
        {
            HandlePan();
        }
        
        if (enableRotation)
        {
            HandleRotation();
        }
    }
    
    void HandleZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        
        if (scrollInput != 0)
        {
            if (mainCamera.orthographic)
            {
                // Orthographic zoom
                mainCamera.orthographicSize -= scrollInput * zoomSpeed;
                mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minZoom, maxZoom);
            }
            else
            {
                // Perspective zoom (move camera forward/backward)
                Vector3 zoomDirection = transform.forward * scrollInput * zoomSpeed;
                transform.position += zoomDirection;
            }
        }
    }
    
    void HandlePan()
    {
        if (Input.GetKeyDown(panKey))
        {
            isPanning = true;
            lastPanPosition = Input.mousePosition;
        }
        
        if (Input.GetKeyUp(panKey))
        {
            isPanning = false;
        }
        
        if (isPanning)
        {
            Vector3 delta = Input.mousePosition - lastPanPosition;
            
            Vector3 move = new Vector3(-delta.x * panSpeed, -delta.y * panSpeed, 0);
            transform.Translate(move * Time.deltaTime, Space.Self);
            
            lastPanPosition = Input.mousePosition;
        }
    }
    
    void HandleRotation()
    {
        if (Input.GetKeyDown(rotateKey))
        {
            isRotating = true;
        }
        
        if (Input.GetKeyUp(rotateKey))
        {
            isRotating = false;
        }
        
        if (isRotating && Input.GetMouseButton(0))
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float rotationY = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            
            transform.Rotate(rotationY, rotationX, 0, Space.World);
        }
    }
    
    /// <summary>
    /// Reset camera to a specified position and rotation
    /// </summary>
    public void ResetCamera(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }
    
    /// <summary>
    /// Focus camera on a specific point
    /// </summary>
    public void FocusOnPoint(Vector3 point)
    {
        transform.LookAt(point);
    }
}
