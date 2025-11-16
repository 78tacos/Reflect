using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles mirror rotation in the puzzle grid.
/// 
/// It is placed on a child object that contains a larger click collider, 
/// allowing the mirror to be easy to select without affecting laser mechanics
/// 
/// The script rotates the parent mirror object, not the child collider, 
/// ensuring the visual mirror and laser reflection collider stay aligned. 
/// 
/// Rotation can be instant or smoothly animated, using a coroutine
/// </summary>

public class MirrorRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationStep = 45f;    // Degrees rotated each step
    public bool smooth = true;
    public float smoothDuration = 0.12f;    // Time for rotation animation

    private bool isRotating = false;

    private Mirror parentMirror;

    void Start()
    {
        parentMirror = GetComponentInParent<Mirror>();
    }

    void OnMouseDown()
    {
        if (Main.Manage.IsPlaying()) return;
        RotateBy(rotationStep);
    }

    public void RotateBy(float degrees)
    {
        if (isRotating) return;    // Prevents rotating while in animation

        Transform mirror = transform.parent;   // Parent object (the real mirror)

        float currentY = mirror.eulerAngles.y;
        float newY = (currentY + degrees) % 360f;

        if (newY < 0) newY += 360f;

        // Smooth or Instant animation
        if (smooth)
            StartCoroutine(RotateCoroutine(mirror, newY));
        else
            mirror.rotation = Quaternion.Euler(0f, newY, 0f);
    }

    private IEnumerator RotateCoroutine(Transform mirror, float targetY)
    {
        isRotating = true;

        Quaternion start = mirror.rotation;
        Quaternion end = Quaternion.Euler(0f, targetY, 0f);
        float t = 0f;

        // Runs duration of the animation
        while (t < smoothDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.SmoothStep(0f, 1f, t / smoothDuration);
            mirror.rotation = Quaternion.Lerp(start, end, alpha);
            yield return null;  // Waits until next frame
        }

        mirror.rotation = end;  // Ensures mirror lands on final rotation
        isRotating = false;

        if ((transform.eulerAngles.y % 90f) == 0f) 
        {
            parentMirror.mirrorType = MirrorType.Reflect;
        } 
        else
        {
            parentMirror.mirrorType = MirrorType.Redirect;
        }
    }
}
