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
}
