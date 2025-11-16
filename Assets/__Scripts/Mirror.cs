using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MirrorType
{
    Redirect,
    Reflect
}

public class Mirror : MonoBehaviour
{

    [Header("Mirror Behavior")]
    public MirrorType mirrorType = MirrorType.Reflect;

    [Header("Reflection")]
    [Tooltip("The direction the mirror's surface is facing")]
    public Vector3 surfaceNormal = Vector3.forward;

    void OnDrawGizmosSelected()
    
    {
        // Draw a cyan line in the Scene view to show the normal's direction
        Gizmos.color = Color.cyan;
        Vector3 worldNormal = transform.TransformDirection(surfaceNormal);
        Gizmos.DrawLine(transform.position, transform.position + worldNormal * 1.0f);
        Gizmos.DrawSphere(transform.position + worldNormal * 1.0f, 0.1f);
    }
}
