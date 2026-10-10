using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObject : MonoBehaviour
{

    public Material normalMaterial;
    public Material highlightMaterial;


    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = normalMaterial;
    }

    void OnMouseEnter() 
    {
        if (Main.Manage.IsPlaying()) return;
        rend.material = highlightMaterial;
    }

    void OnMouseExit() 
    {
        if (Main.Manage.IsPlaying()) return;
        rend.material = normalMaterial;
    }

}
