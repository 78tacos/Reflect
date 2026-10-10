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
}
