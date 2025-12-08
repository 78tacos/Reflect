using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int row = 4;
    public int column = 4;
    private float spacing = 0.7f;
    private float scale = 0.3f;

    [Header("Grid Positioning")]
    [Tooltip("Height offset from the surface (negative values go below surface)")]
    public float heightOffset = 0f;

    public GameObject osCubePrefab;

#if UNITY_EDITOR
[ContextMenu("Generate Grid")]
    void GenerateGrid() 
    {
        Vector3 cubeScale = Vector3.one * scale;
        float offsetX = (row - 1) * spacing * 0.5f;
        float offsetZ = (column - 1) * spacing * 0.5f;

        // Get the camera and raycast to find where it's looking
        Camera mainCamera = Camera.main;
        Vector3 centerPos = Vector3.zero;
        float gridY = 0f;

        if (mainCamera != null)
        {
            // Cast a ray from camera forward
            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f))
            {
                // Grid centers at the hit point
                centerPos = hit.point;
                gridY = hit.point.y + heightOffset;
                Debug.Log($"Grid placed at raycast hit: {hit.point}, surface: {hit.collider.name}");
            }
            else
            {
                // If no hit, place at a distance in front of camera
                centerPos = mainCamera.transform.position + mainCamera.transform.forward * 10f;
                gridY = centerPos.y + heightOffset;
                Debug.Log($"No raycast hit, placing grid in front of camera at: {centerPos}");
            }
        }

        for (int i = 0; i < row; i++) {
            for (int j = 0; j < column; j++) {
                Vector3 pos = new Vector3(
                    centerPos.x + (i * spacing - offsetX), 
                    gridY, 
                    centerPos.z + (j * spacing - offsetZ)
                );
                GameObject cube = Instantiate(osCubePrefab, pos, Quaternion.identity, transform);

                cube.transform.localScale = cubeScale;
            }
        }
    }
#endif
}