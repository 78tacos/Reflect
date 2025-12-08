using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nullBlockFeat : MonoBehaviour
{
    public GameObject turret;
    public GameObject goal;
    public GameObject nullBlock;

    [Header("Cube Swap (Editor Only)")]
    [Tooltip("The OS Cube prefab to swap this null cube with")]
    public GameObject osCubePrefab;

    #if UNITY_EDITOR
    [ContextMenu("Spawn Turret")]
    void SpawnTurret() 
    {
        Vector3 spawnPos = transform.position + (Vector3.up * 0.27f);
        Instantiate(turret, spawnPos, Quaternion.identity);
    }

    [ContextMenu("Spawn Goal")]
    void SpawnGoal() 
    {
        Vector3 spawnPos = transform.position + (Vector3.up * 0.6f);
        Instantiate(goal, spawnPos, Quaternion.identity);
    }

    [ContextMenu("Spawn Null Block")]
    void SpawnNullBlock() 
    {
        Vector3 spawnPos = transform.position + (Vector3.up * 0.6f);
        Instantiate(nullBlock, spawnPos, Quaternion.identity);
    }

    [ContextMenu("Swap to OS Cube")]
    void SwapToOSCube()
    {
        if (osCubePrefab == null)
        {
            Debug.LogWarning("No OS Cube prefab assigned! Assign it in the inspector first.");
            return;
        }

        // Store current position, rotation, and scale
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;
        Vector3 currentScale = transform.localScale;
        Transform currentParent = transform.parent;
        int siblingIndex = transform.GetSiblingIndex();

        // Instantiate the OS cube at the same position
        GameObject newCube = Instantiate(osCubePrefab, currentPosition, currentRotation, currentParent);
        newCube.transform.localScale = currentScale;
        newCube.transform.SetSiblingIndex(siblingIndex);

        // Destroy this null cube
        DestroyImmediate(gameObject);

        Debug.Log($"Swapped null cube to OS cube at position {currentPosition}");
    }

    #endif
}

