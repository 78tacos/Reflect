using UnityEngine;

/// <summary>
/// Represents a light beam that can reflect off mirrors
/// </summary>
public class LightBeam : MonoBehaviour
{
    [Header("Beam Properties")]
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private int maxReflections = 10;
    [SerializeField] private float beamWidth = 0.1f;
    [SerializeField] private Color beamColor = Color.yellow;
    [SerializeField] private float reflectionOffset = 0.01f; // Small offset to avoid self-collision
    
    private LineRenderer lineRenderer;
    private LightSource lightSource;
    
    void Awake()
    {
        // Setup LineRenderer for visual beam
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        
        ConfigureLineRenderer();
    }
    
    void ConfigureLineRenderer()
    {
        lineRenderer.startWidth = beamWidth;
        lineRenderer.endWidth = beamWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = beamColor;
        lineRenderer.endColor = beamColor;
        lineRenderer.positionCount = 0;
    }
    
    public void SetLightSource(LightSource source)
    {
        lightSource = source;
    }
    
    public void UpdateBeam(Vector3 startPosition, Vector3 direction)
    {
        Vector3[] beamPoints = CalculateBeamPath(startPosition, direction);
        
        lineRenderer.positionCount = beamPoints.Length;
        lineRenderer.SetPositions(beamPoints);
        
        // Check if beam hits target
        CheckTargetHit(beamPoints);
    }
    
    private Vector3[] CalculateBeamPath(Vector3 startPosition, Vector3 direction)
    {
        System.Collections.Generic.List<Vector3> points = new System.Collections.Generic.List<Vector3>();
        points.Add(startPosition);
        
        Vector3 currentPosition = startPosition;
        Vector3 currentDirection = direction.normalized;
        int reflectionsCount = 0;
        
        while (reflectionsCount < maxReflections)
        {
            RaycastHit hit;
            if (Physics.Raycast(currentPosition, currentDirection, out hit, maxDistance))
            {
                points.Add(hit.point);
                
                // Check if we hit a mirror
                if (hit.collider.CompareTag("Mirror"))
                {
                    // Calculate reflection direction
                    currentDirection = Vector3.Reflect(currentDirection, hit.normal);
                    currentPosition = hit.point + currentDirection * reflectionOffset;
                    reflectionsCount++;
                }
                else
                {
                    // Hit something else, stop the beam
                    break;
                }
            }
            else
            {
                // No hit, extend beam to max distance
                points.Add(currentPosition + currentDirection * maxDistance);
                break;
            }
        }
        
        return points.ToArray();
    }
    
    private void CheckTargetHit(Vector3[] beamPoints)
    {
        if (beamPoints.Length < 2) return;
        
        for (int i = 0; i < beamPoints.Length - 1; i++)
        {
            Vector3 start = beamPoints[i];
            Vector3 end = beamPoints[i + 1];
            Vector3 direction = (end - start).normalized;
            float distance = Vector3.Distance(start, end);
            
            RaycastHit hit;
            if (Physics.Raycast(start, direction, out hit, distance))
            {
                if (hit.collider.CompareTag("Target"))
                {
                    Target target = hit.collider.GetComponent<Target>();
                    if (target != null)
                    {
                        target.OnLightHit();
                    }
                }
            }
        }
    }
    
    public void SetBeamColor(Color color)
    {
        beamColor = color;
        if (lineRenderer != null)
        {
            lineRenderer.startColor = beamColor;
            lineRenderer.endColor = beamColor;
        }
    }
    
    public void SetBeamWidth(float width)
    {
        beamWidth = width;
        if (lineRenderer != null)
        {
            lineRenderer.startWidth = beamWidth;
            lineRenderer.endWidth = beamWidth;
        }
    }
}
