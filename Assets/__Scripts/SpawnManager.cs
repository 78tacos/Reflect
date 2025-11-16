using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<SpawnLaser> turretPrefabs = new List<SpawnLaser>();

    // void Start () 
    // {

    // }

    #if UNITY_EDITOR
    [ContextMenu("Spawn Lasers")]
    void SpawnLasers() 
    {
        foreach (var item in turretPrefabs) 
        {
            item.Spawn();
        }
    }
    #endif
}
