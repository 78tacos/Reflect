using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class HighlightableCube : MonoBehaviour
{
[Header("Highlight Settings")]
    public Color highlightColor = Color.yellow;
    
    [Tooltip("Set this to the layer your cubes are on.")]
    public LayerMask cubeLayer; // We'll set this in the Inspector

    // This static (shared) list tracks all currently highlighted cubes
    private static List<HighlightableCube> s_highlightedCubes = new List<HighlightableCube>();

    // Private references for this specific cube
    private MeshRenderer m_renderer;
    private Color m_originalColor;
    private float m_rayDistance;

    void Start()
    {
        // Get this cube's renderer
        m_renderer = GetComponent<MeshRenderer>();
        // Store its starting color (gray)
        m_originalColor = m_renderer.material.color;

        // Calculate a ray distance just longer than one cube
        m_rayDistance = transform.lossyScale.x * 1.5f;
    }

    /// <summary>
    /// This runs when the collider is clicked
    /// </summary>
    void OnMouseDown()
    {
        // 1. Clear all previously highlighted cubes
        ClearAllHighlights();

        // 2. Find and highlight all neighbors of *this* cube
        FindAndHighlightNeighbors();
    }

    /// <summary>
    /// * Uses 4 raycasts to find neighbors and tells them to highlight
    /// </summary>
    private void FindAndHighlightNeighbors()
    {
        // The 4 directions to check (forward, back, left, right)
        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };

        foreach (Vector3 dir in directions)
        {
            // Shoot a short ray from this cube's center in one direction
            Ray ray = new Ray(transform.position, dir);
            
            // Check if we hit another cube on the "Cubes" layer
            if (Physics.Raycast(ray, out RaycastHit hit, m_rayDistance, cubeLayer))
            {
                // Get the script from the cube we hit
                HighlightableCube neighbor = hit.collider.GetComponent<HighlightableCube>();

                if (neighbor != null)
                {
                    // Tell the neighbor to turn yellow
                    neighbor.Highlight();
                    // Add it to our list so we can clear it later
                    s_highlightedCubes.Add(neighbor);
                }
            }
        }
    }

    // --- Public Helper Methods ---

    /// <summary>
    /// Changes this cube's color to the highlight color
    /// </summary>
    public void Highlight()
    {
        if (m_renderer != null)
        {
            m_renderer.material.color = highlightColor;
        }
    }

    /// <summary>
    /// Resets this cube's color to its original color
    /// </summary>
    public void ClearHighlight()
    {
        if (m_renderer != null)
        {
            m_renderer.material.color = m_originalColor;
        }
    }

    /// <summary>
    /// A static (shared) method that tells all cubes in the list to reset.
    /// </summary>
    public static void ClearAllHighlights()
    {
        foreach (HighlightableCube cube in s_highlightedCubes)
        {
            cube.ClearHighlight();
        }
        s_highlightedCubes.Clear();
    }
}
