using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nullBlockFeat : MonoBehaviour
{
    public GameObject turret;
    public GameObject goal;


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

    #endif
}

