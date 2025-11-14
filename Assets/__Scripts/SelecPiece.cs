using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecPiece : MonoBehaviour
{

    [SerializeField]
    private bool hasClicked = false;

    void Start ()
    {
        hasClicked = false;
    }

    void OnMouseDown()
    {
        Debug.Log("This piece has been clicked.");
        this.hasClicked = true;
    }
}
