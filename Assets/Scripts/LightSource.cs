using UnityEngine;

/// <summary>
/// Light source that emits a beam in a specified direction
/// </summary>
public class LightSource : MonoBehaviour
{
    [Header("Light Properties")]
    [SerializeField] private Vector3 emissionDirection = Vector3.forward;
    [SerializeField] private Color lightColor = Color.yellow;
    [SerializeField] private float beamWidth = 0.1f;
    
    private LightBeam lightBeam;
    
    void Start()
    {
        InitializeLightBeam();
    }
    
    void InitializeLightBeam()
    {
        // Create light beam GameObject
        GameObject beamObject = new GameObject("LightBeam");
        beamObject.transform.SetParent(transform);
        beamObject.transform.localPosition = Vector3.zero;
        
        lightBeam = beamObject.AddComponent<LightBeam>();
        lightBeam.SetLightSource(this);
        lightBeam.SetBeamColor(lightColor);
        lightBeam.SetBeamWidth(beamWidth);
    }
    
    void Update()
    {
        if (lightBeam != null)
        {
            // Update beam from source position in the emission direction
            Vector3 worldDirection = transform.TransformDirection(emissionDirection.normalized);
            lightBeam.UpdateBeam(transform.position, worldDirection);
        }
    }
    
    public void SetEmissionDirection(Vector3 direction)
    {
        emissionDirection = direction.normalized;
    }
    
    public Vector3 GetEmissionDirection()
    {
        return emissionDirection;
    }
    
    void OnDrawGizmos()
    {
        // Draw emission direction in editor
        Gizmos.color = Color.yellow;
        Vector3 worldDirection = transform.TransformDirection(emissionDirection.normalized);
        Gizmos.DrawRay(transform.position, worldDirection * 2f);
    }
}
