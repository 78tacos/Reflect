using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int row = 4;
    public int column = 4;
    private float spacing = 0.7f;
    private float scale = 0.3f;

    public GameObject osCubePrefab;
    private Vector3 cubeScale;

    #if UNITY_EDITOR
    [ContextMenu("Generate Grid")]
    void GenerateGrid() 
    {
        cubeScale = Vector3.one * scale;
        float offsetX = (row - 1) * spacing * 0.5f;
        float offsetZ = (column - 1) * spacing * 0.5f;

        for (int i = 0; i < row; i++) {
            for (int j = 0; j < column; j++) {
                Vector3 pos = new Vector3(i * spacing - offsetX, -12f, j * spacing - offsetZ);
                GameObject cube = Instantiate(osCubePrefab, pos, Quaternion.identity, transform);

                cube.transform.localScale = cubeScale;
            }
        }
    }
}
#endif