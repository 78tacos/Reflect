using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages game state, level progression, and win conditions
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private string nextLevelName;
    [SerializeField] private bool autoAdvance = true;
    [SerializeField] private float winDelay = 2f;
    
    [Header("UI References")]
    [SerializeField] private GameObject winPanel;
    
    private Target[] targets;
    private bool levelComplete = false;
    private float winTimer = 0f;
    
    void Start()
    {
        // Find all targets in the scene
        targets = FindObjectsOfType<Target>();
        
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
        
        Debug.Log($"Game Manager initialized. Found {targets.Length} target(s).");
    }
    
    void Update()
    {
        if (levelComplete && autoAdvance)
        {
            winTimer += Time.deltaTime;
            if (winTimer >= winDelay)
            {
                LoadNextLevel();
            }
        }
        
        // Quick restart with R key
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
        
        // Next level with N key
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadNextLevel();
        }
    }
    
    public void OnTargetStateChanged()
    {
        if (levelComplete) return;
        
        // Check if all targets are hit
        bool allTargetsHit = true;
        foreach (Target target in targets)
        {
            if (!target.IsHit())
            {
                allTargetsHit = false;
                break;
            }
        }
        
        if (allTargetsHit && targets.Length > 0)
        {
            OnLevelComplete();
        }
    }
    
    private void OnLevelComplete()
    {
        levelComplete = true;
        winTimer = 0f;
        
        Debug.Log("Level Complete!");
        
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.Log("No next level specified. Use 'R' to restart.");
        }
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    
    public int GetTargetCount()
    {
        return targets != null ? targets.Length : 0;
    }
    
    public int GetHitTargetCount()
    {
        if (targets == null) return 0;
        
        int count = 0;
        foreach (Target target in targets)
        {
            if (target.IsHit())
            {
                count++;
            }
        }
        return count;
    }
}
