using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLaser : MonoBehaviour
{
    public GameObject laserPrefab;

    public void Spawn() {

        Vector3 euler = Quaternion.identity.eulerAngles;
        euler.y = transform.rotation.eulerAngles.y;
        euler.x = 90f;

        Vector3 spawnPos = (Vector3.up * 0.4f) + transform.position;

        Instantiate(laserPrefab, spawnPos, Quaternion.Euler(euler));

    }
}
