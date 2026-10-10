using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Material notHitMaterial;
    public Material hitMaterial;

    private Renderer rend;
    public bool isActivated;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = notHitMaterial;
        isActivated = false;
    }

    public void GoalHit()
    {
        if (isActivated) return;

        rend.material = hitMaterial;
        this.isActivated = true;

        Main.Manage.incGoalHit();
        Main.Manage.goalsHit_display.Add(rend);
    }
}
