using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public enum NormalMode
    {
        TransformAxis,
        CustomVector
    }

    public enum TransformAxis
    {
        Forward,
        Backward,
        Up,
        Down,
        Right,
        Left
    }

    [Header("Reflection")]
    [Tooltip("How the surface normal should be determined.")]
    public NormalMode normalMode = NormalMode.TransformAxis;
    [Tooltip("Which transform axis represents the reflective normal.")]
    public TransformAxis axis = TransformAxis.Forward;
    [Tooltip("The direction the mirror's surface is facing when using a custom vector.")]
    public Vector3 surfaceNormal = Vector3.back;

    /// <summary>
    /// Returns the mirror's surface normal in world space.
    /// </summary>
    public Vector3 GetWorldNormal()
    {
        if (normalMode == NormalMode.TransformAxis)
        {
            return GetAxisDirection(axis);
        }

        Vector3 normal = surfaceNormal.sqrMagnitude > Mathf.Epsilon
            ? surfaceNormal.normalized
            : Vector3.forward;

        return transform.TransformDirection(normal);
    }

    private Vector3 GetAxisDirection(TransformAxis selectedAxis)
    {
        switch (selectedAxis)
        {
            case TransformAxis.Forward:
                return transform.forward;
            case TransformAxis.Backward:
                return -transform.forward;
            case TransformAxis.Up:
                return transform.up;
            case TransformAxis.Down:
                return -transform.up;
            case TransformAxis.Right:
                return transform.right;
            case TransformAxis.Left:
                return -transform.right;
            default:
                return transform.forward;
        }
    }
}
