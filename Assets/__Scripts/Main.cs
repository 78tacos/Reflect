using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{

    public static Main Manage; 

    public List<SpawnLaser> turretPrefabs = new List<SpawnLaser>();
    public List<Goal> goalsList = new List<Goal>();
    public int numGoals = 1;
    public int maxMirrors = 2;
    private int goals;
    private int mirrorCount;
    private bool isPlaying;
    private Button storedPlayButton;

    private List<GameObject> activeLasers = new List<GameObject>();

    public Text txtTrackMirrorCount;

    void Awake()
    {
        Manage = this;
        goals = 0;
        mirrorCount = 0;
        isPlaying = false;
        updateMirrorTxt();

        goalsList.AddRange(FindObjectsOfType<Goal>());
    }

    public void incGoalHit()
    {
        goals++;

        if (goals == numGoals) {
            Debug.Log("End game sequence");
        }
    }

    public bool MirrorCountFull() => (mirrorCount == maxMirrors);
    public bool IsPlaying() => (isPlaying);

    public void IncMirror()
    {
        mirrorCount++;
        updateMirrorTxt();
    }

    public void decMirror()
    {
        mirrorCount--;
        updateMirrorTxt();
    }

    [ContextMenu("Spawn Lasers")]
    public void SpawnLasers() 
    {
        isPlaying = true;
        activeLasers.Clear();

        foreach (var item in turretPrefabs) 
        {
            GameObject laser = item.Spawn();
            activeLasers.Add(laser);
        }
    }

    public void LevelReset()
    {
        GameObject[] allLasers = GameObject.FindGameObjectsWithTag("Laser");

        foreach (GameObject laser in allLasers)
        {
            Destroy(laser);
        }

        activeLasers.Clear();
        isPlaying = false;
        goals = 0;  // Reset goal counter when no lasers are shown

        // Reactivate play button
        if (storedPlayButton != null)
        {
            storedPlayButton.gameObject.SetActive(true);
        }

        // Reset goals
        foreach (var goal in goalsList)
        {
            goal.ResetGoal();
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void PlayButton(Button clickedButton)
    {
        storedPlayButton = clickedButton;
        clickedButton.gameObject.SetActive(false);
        SpawnLasers();
    }

    private void updateMirrorTxt()
    {
        txtTrackMirrorCount.text = "Mirrors Used: " + mirrorCount + "/" + maxMirrors;
    }
}