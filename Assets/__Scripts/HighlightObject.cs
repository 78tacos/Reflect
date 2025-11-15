using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{

    public Material normalMaterial;
    public Material highlightMaterial;


    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = normalMaterial;
    }

    void OnMouseEnter() 
    {
        rend.material = highlightMaterial;
    }

    void OnMouseExit() 
    {
        rend.material = normalMaterial;
    }

}
