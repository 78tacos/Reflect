using UnityEngine;

/// <summary>
/// Represents a target that needs to be hit by light to complete the level
/// </summary>
[RequireComponent(typeof(Collider))]
public class Target : MonoBehaviour
{
    [Header("Target Properties")]
    [SerializeField] private Color inactiveColor = Color.red;
    [SerializeField] private Color activeColor = Color.green;
    [SerializeField] private float glowIntensity = 2f;
    
    private bool isHit = false;
    private Renderer targetRenderer;
    private Material targetMaterial;
    private GameManager gameManager;
    
    void Start()
    {
        // Ensure this object has the Target tag
        if (!gameObject.CompareTag("Target"))
        {
            gameObject.tag = "Target";
        }
        
        // Setup visual appearance
        targetRenderer = GetComponent<Renderer>();
        if (targetRenderer != null)
        {
            targetMaterial = targetRenderer.material;
            UpdateVisual();
        }
        
        // Find game manager
        gameManager = FindObjectOfType<GameManager>();
    }
    
    void Update()
    {
        // Reset hit state each frame (will be set again if light hits)
        bool wasHit = isHit;
        isHit = false;
        
        // Update visual if state changed
        if (wasHit != isHit)
        {
            UpdateVisual();
            if (gameManager != null)
            {
                gameManager.OnTargetStateChanged();
            }
        }
    }
    
    public void OnLightHit()
    {
        if (!isHit)
        {
            isHit = true;
            UpdateVisual();
            if (gameManager != null)
            {
                gameManager.OnTargetStateChanged();
            }
        }
    }
    
    private void UpdateVisual()
    {
        if (targetMaterial != null)
        {
            Color currentColor = isHit ? activeColor : inactiveColor;
            targetMaterial.color = currentColor;
            
            // Add emission for glow effect
            if (targetMaterial.HasProperty("_EmissionColor"))
            {
                targetMaterial.EnableKeyword("_EMISSION");
                targetMaterial.SetColor("_EmissionColor", currentColor * (isHit ? glowIntensity : 0.5f));
            }
        }
    }
    
    public bool IsHit()
    {
        return isHit;
    }
    
    void OnDrawGizmos()
    {
        // Draw target sphere in editor
        Gizmos.color = isHit ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
