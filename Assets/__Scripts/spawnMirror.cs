using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnMirror : MonoBehaviour
{
    public GameObject mirrorPrefab;
    private GameObject mirrorRef;
    public float heightOffset = 1.5f;

    public void OnMouseOver()
    {

        if (Input.GetMouseButtonDown(0))
        {
            SpawnMirror();
        } else if (Input.GetMouseButtonDown(1))
        {
            DestroyMirror();
        }
    }

    void SpawnMirror()
    {
        if (mirrorRef != null || mirrorPrefab == null) return;
        Vector3 spawnPos = transform.position + (Vector3.up * heightOffset);
        mirrorRef = Instantiate(mirrorPrefab, spawnPos, Quaternion.identity);
    }

    void DestroyMirror()
    {
        if (this.mirrorRef == null) return;
        Destroy(this.mirrorRef);
    }
}
