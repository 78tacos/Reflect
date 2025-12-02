using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{

    public static Main Manage; 

    public List<SpawnLaser> turretPrefabs = new List<SpawnLaser>();
    public List<GameObject> lasers;
    public List<Renderer> goalsHit_display;
    public Material notHitMaterial;

    public Button playButton;
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
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        lasers = new List<GameObject>();
        goalsHit_display = new List<Renderer>();
    }

    public void incGoalHit()
    {
        goals++;

        if (goals == numGoals) {
            SceneManager.LoadScene("c_Level");
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
            lasers.Add(item.Spawn());
        }
    }

    public void LevelReset()
    {
        if (!isPlaying) return;

        if (playButton != null) playButton.gameObject.SetActive(true);

        foreach (var item in lasers)
        {
            Destroy(item);
        }

        foreach (var item in goalsHit_display)
        {
            item.material = notHitMaterial;
            item.GetComponent<Goal>().isActivated = false;
        }

        goalsHit_display = new List<Renderer>();
        goals = 0;
        isPlaying = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void PlayButton(Button clickedButton)
    {
        playButton = clickedButton;
        clickedButton.gameObject.SetActive(false);
        SpawnLasers();
    }

    private void updateMirrorTxt()
    {
        txtTrackMirrorCount.text = "Mirrors Used: " + mirrorCount + "/" + maxMirrors;
    }
}