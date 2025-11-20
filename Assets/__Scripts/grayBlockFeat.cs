using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class grayBlockFeat : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject nullBlock;

    [ContextMenu("Replace as Null Block")]
    void SpawnNullBlock() 
    {
        Vector3 spawnPos = transform.position;
        GameObject newObj = Instantiate(nullBlock, spawnPos, Quaternion.identity);
        DestroyImmediate(gameObject);
    }

}
