using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update

    public Material notHitMaterial;
    public Material hitMaterial;

    private Renderer rend;
    private bool isActivated;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = notHitMaterial;
        isActivated = false;
    }

    public void GoalHit()
    {
        if (isActivated) return;

        //Debug.Log("Goal has been hit.");
        rend.material = hitMaterial;
        this.isActivated = true;

        Main.Manage.incGoalHit();
    }
}
