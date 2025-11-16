using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Set the Build Index of your Level 1 scene
    // [SerializeField] private int level1BuildIndex = 1; // 0 = MainMenu, 1 = Level 1

    void Start()
    {
        // Ensure time is normal
        // Time.timeScale = 1f;
    }

    // public void PlayGame()
    // {
    //     // Check if the index is valid
    //     if (level1BuildIndex >= 0 && level1BuildIndex < SceneManager.sceneCountInBuildSettings)
    //     {
    //         SceneManager.LoadScene(level1BuildIndex);
    //     }
    //     else
    //     {
    //         Debug.LogError("Invalid build index for Level 1! Check Build Settings.");
    //     }
    // }

    // ############################################################
    // ############################################################
    // GO BACK AND CHANGE THIS AFTER ALPHA,
    // THIS IS JUST TO LOAD INTO LEVEL1 ONLY
    // ############################################################
    // ############################################################

    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }

}
