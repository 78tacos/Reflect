using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [Header("Reflection")]
    [Tooltip("The direction the mirror's surface is facing")]
    public Vector3 surfaceNormal = Vector3.back;
}
