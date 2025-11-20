using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class nullBlockFeat : MonoBehaviour
{
    public GameObject turret;
    public GameObject goal;
    public GameObject nullBlock;
    public GameObject osCube;


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

    [ContextMenu("Spawn Null Cube")]
    void SpawnNullBlock() 
    {
        Vector3 spawnPos = transform.position + (Vector3.up * 0.6f);
        Instantiate(nullBlock, spawnPos, Quaternion.identity);
    }

    [ContextMenu("Replace as Grey Selective Cube")]
    void SpawnGrayCube() 
    {
        Vector3 spawnPos = transform.position;
        GameObject newObj = Instantiate(osCube, spawnPos, Quaternion.identity);
        Selection.activeGameObject = newObj;
        DestroyImmediate(gameObject);
    }

    #endif
}

