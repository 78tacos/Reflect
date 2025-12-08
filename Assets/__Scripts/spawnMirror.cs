using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnMirror : MonoBehaviour
{
    public GameObject mirrorPrefab;
    private GameObject mirrorRef;
    public float heightOffset = 1.5f;

    [Header("Cube Swap (Editor Only)")]
    [Tooltip("The prefab to swap this cube with (OS Cube <-> Null Cube)")]
    public GameObject alternateCubePrefab;

    public void OnMouseOver()
    {

        if (Main.Manage.IsPlaying()) return;

        if (Input.GetMouseButtonDown(0))
        {
            SpawnMirror();
        } 
        else if (Input.GetMouseButtonDown(1))
        {
            DestroyMirror();
        }
    }

    void SpawnMirror()
    {
        if (mirrorRef != null || mirrorPrefab == null) return;

        if (Main.Manage.MirrorCountFull())
        {
            Debug.Log("Reached max mirrors! Returning...");
            return;
        }


        Vector3 spawnPos = transform.position + (Vector3.up * heightOffset);
        mirrorRef = Instantiate(mirrorPrefab, spawnPos, Quaternion.identity);
        Main.Manage.IncMirror();
    }

    void DestroyMirror()
    {
        if (this.mirrorRef == null) return;
        Destroy(this.mirrorRef);
        Main.Manage.decMirror();
    }

#if UNITY_EDITOR
    [ContextMenu("Swap Cube Type")]
    void SwapCubeType()
    {
        if (alternateCubePrefab == null)
        {
            Debug.LogWarning("No alternate cube prefab assigned! Assign it in the inspector first.");
            return;
        }

        // Store current position, rotation, and scale
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;
        Vector3 currentScale = transform.localScale;
        Transform currentParent = transform.parent;
        int siblingIndex = transform.GetSiblingIndex();

        // Instantiate the alternate cube at the same position
        GameObject newCube = Instantiate(alternateCubePrefab, currentPosition, currentRotation, currentParent);
        newCube.transform.localScale = currentScale;
        newCube.transform.SetSiblingIndex(siblingIndex);

        // Destroy this cube
        DestroyImmediate(gameObject);

        Debug.Log($"Swapped cube at position {currentPosition}");
    }
#endif
}
