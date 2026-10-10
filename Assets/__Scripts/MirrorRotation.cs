using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates the parent mirror from a child with a larger click collider,
/// so the mirror is easy to select without changing the laser hit volume.
/// </summary>

public class MirrorRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationStep = 45f;
    public bool smooth = true;
    public float smoothDuration = 0.12f;

    private bool isRotating = false;

    private Mirror parentMirror;

    void Start()
    {
        parentMirror = GetComponentInParent<Mirror>();
    }

    public void OnMouseOver()
    {

        if (Main.Manage.IsPlaying()) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                Destroy(transform.parent.gameObject);
                Main.Manage.decMirror();
                return;
            } else
            {
                RotateBy(rotationStep);
            }
        } 
        else if (Input.GetMouseButtonDown(1))
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                Destroy(transform.parent.gameObject);
                Main.Manage.decMirror();
                return;
            } else
            {
                RotateBy(-rotationStep);
            }
        }
    }

    public void RotateBy(float degrees)
    {
        if (isRotating) return;    // Prevents rotating while in animation

        Transform mirror = transform.parent;   // Parent object (the real mirror)

        float currentY = mirror.eulerAngles.y;

        parentMirror.mirrorType = (parentMirror.mirrorType == MirrorType.Reflect) ? MirrorType.Redirect : MirrorType.Reflect;

        float newY = (currentY + degrees) % 360f;

        if (newY < 0) newY += 360f;

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

        while (t < smoothDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.SmoothStep(0f, 1f, t / smoothDuration);
            mirror.rotation = Quaternion.Lerp(start, end, alpha);
            yield return null;
        }

        mirror.rotation = end;  // Ensures mirror lands on final rotation
        isRotating = false;
    }
}
