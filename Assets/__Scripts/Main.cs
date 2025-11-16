using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    public static Main Manage;

    public int numGoals = 1;
    private int goals;

    void Start()
    {
        Manage = this;
        goals = 0;
    }

    public void incGoalHit()
    {
        goals++;

        if (goals == numGoals) {
            Debug.Log("End game sequence");
        }
    }
}
