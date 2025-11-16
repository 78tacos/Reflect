using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{

    public static Main Manage; 

    public List<SpawnLaser> turretPrefabs = new List<SpawnLaser>();
    public int numGoals = 1;
    public int maxMirrors = 2;
    private int goals;
    private int mirrorCount;
    private bool isPlaying;

    public Text txtTrackMirrorCount;

    void Awake()
    {
        Manage = this;
        goals = 0;
        mirrorCount = 0;
        isPlaying = false;
        updateMirrorTxt();
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
        foreach (var item in turretPrefabs) 
        {
            item.Spawn();
        }
    }

    public void LevelReset()
    {
        Debug.Log("Add scene manager");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void PlayButton(Button clickedButton)
    {
        clickedButton.gameObject.SetActive(false);
        SpawnLasers();
    }

    private void updateMirrorTxt()
    {
        txtTrackMirrorCount.text = "Mirrors Used: " + mirrorCount + "/" + maxMirrors;
    }
}